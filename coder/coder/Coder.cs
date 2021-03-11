using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using StreamJsonRpc;

using coder.ModelBridge;

namespace coder
{
    class Coder
    {
        enum CoderState
        {
            IDLE,      // Простой
            SETTINGS,  // Режим настройки
            WORKING,   // Выполняется команда
            DONE,      // Вся программа выполнена
        }

        enum SettingMode
        {
            AUTO,    // Автоматический режим выполнения команд
            MANUAL,  // Пошаговый режим выполнения команд
        }

        private InstructionHandler handler;

        public Coder(string program, InstructionHandler h)
        {
            ProgramCode = new List<string>();
            Parameters = new Dictionary<string, int>();
            SetDfltParams();
            State = CoderState.IDLE;
            mode = SettingMode.AUTO;
            using (StringReader s = new StringReader(program))
            {
                string l;
                while ((l = s.ReadLine()) != null)
                {
                    if (l.Trim().Length == 0)
                    {
                        continue;
                    }

                    ProgramCode.Add(l);
                }
            }
            PC = 0;
            handler = h;
        }

        [JsonRpcMethod("coder.set_parameter")]
        public void SetParameter(string name, int val)
        {
            if (State != CoderState.SETTINGS || mode != SettingMode.MANUAL)
            {
                return;
            }
            Console.WriteLine("Called: setParameter(" + name + ", " + val + ")");
            Parameters[name] = val;
        }

        [JsonRpcMethod("coder.press_key")]
        public void PressKey(string key)
        {
            Console.WriteLine("Called: press_key(" + key + ")");

            switch (key)
            {
                case "enter":
                    break;
                case "start":
                    if (curTask != null && !curTask.IsCompleted)
                    {
                        curTask.Wait();
                    }
                    State = CoderState.WORKING;
                    PC = 0;
                    curTask = Task.Run(ExecuteProgram);
                    break;
                case "stop":
                    State = CoderState.DONE;
                    PC = 0;
                    break;
                case "setting":
                    State = CoderState.SETTINGS;
                    break;
                case "auto_mode":
                    mode = SettingMode.AUTO;
                    break;
                case "manual_mode":
                    mode = SettingMode.MANUAL;
                    break;
                default:
                    Console.WriteLine("Warning: ignoring unknown key code");
                    break;
            }
        }

        private void SetDfltParams()
        {
            Parameters["XMIN"] = 3;
            Parameters["XMAX"] = 20;
            Parameters["YMIN"] = 1;
            Parameters["YMAX"] = 4;
            Parameters["ZMIN"] = 1;
            Parameters["ZMAX"] = 2;
            Parameters["TZAD"] = 1;

            // Private params for internal use only
            Parameters["X"] = 3;
            Parameters["Y"] = 1;
            Parameters["Z"] = 1;
        }

        private bool IsOutOfBounds(string axis, int newValue)
        {
            int maxValue = newValue;
            int minValue = newValue;
            switch (axis)
            {
                case "X":
                    maxValue = Parameters["XMAX"];
                    minValue = Parameters["XMIN"];
                    break;
                case "Y":
                    maxValue = Parameters["YMAX"];
                    minValue = Parameters["YMIN"];
                    break;
                case "Z":
                    maxValue = Parameters["ZMAX"];
                    minValue = Parameters["ZMIN"];
                    break;
            }

            return newValue > maxValue || newValue < minValue;
        }

        private void ExecuteSET(string axis, int value)
        {
            bool s = Parameters.TryGetValue(axis, out int cur);
            if (!s)
            {
                Console.WriteLine("Warning: wrong syntax of SET opcode, ignoring");
                return;
            }

            bool pos = value > cur;
            int secPerUnit = Parameters["TZAD"]; // 1 by default
            int sec = Math.Abs(value - cur) / secPerUnit;
            
            for (int i = 0; i < sec; ++i)
            {
                int newValue = (pos) ? ++cur : --cur;
                if (IsOutOfBounds(axis, newValue))
                {
                    Console.WriteLine("Warning: out of bound on " + axis + " axis, not doing that");
                    break;
                }
                handler.Move(axis, pos);
                Parameters[axis] = newValue;
                Thread.Sleep(secPerUnit/* * 1000*/);
            }
            handler.Stop();
        }

        private void ExecuteLine(string line)
        {
            string[] cmd = line.Split();
            string opCode = cmd[0];
            switch (opCode)
            {
                case "SET":
                    if (cmd.Length != 3)
                    {
                        Console.WriteLine("Warning: wrong syntax of SET opcode, ignoring");
                        break;
                    }
                    int val;
                    try
                    {
                        val = Convert.ToInt32(cmd[2]);
                    } catch (Exception)
                    {
                        Console.WriteLine("Warning: wrong syntax of SET opcode, ignoring");
                        break;
                    }
                    ExecuteSET(cmd[1], val);
                    break;
                case "FOR":
                    Console.WriteLine("Warning: FOR opcode is not supported");
                    break;
                case "END":
                    Console.WriteLine("Warning: END opcode is not supported");
                    break;
                default:
                    Console.WriteLine("Warning: ignored unknown opcode " + opCode);
                    break;
            }
        }

        private void ExecuteProgram()
        {
            if (mode == SettingMode.MANUAL)
            {
                ExecuteLine(ProgramCode[PC]);
                State = CoderState.IDLE;
                return;
            }
            
            foreach(var l in ProgramCode)
            {
                ExecuteLine(l);
            }
        }

        private Task curTask;

        private readonly Dictionary<string, int> Parameters;
        private CoderState State;
        private SettingMode mode;

        private int PC;
        private List<string> ProgramCode;
    }
}
