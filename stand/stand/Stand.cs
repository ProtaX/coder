using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using StreamJsonRpc;

namespace stand
{
    class Stand
    {
        private InstructionHandler handler;

        public Stand(InstructionHandler h)
        {
            handler = h;
            KnifeMoving = false;
            t = Task.Run(KnifeTask);
        }

        [JsonRpcMethod("stand.move_knife")]
        public void MoveKnife(string axis, bool positive)
        {
            Console.WriteLine("Started moving on " + axis + ", dir: " + positive);
            this.axis = axis;
            this.pos = positive;
            KnifeMoving = true;
        }

        [JsonRpcMethod("stand.stop_knife")]
        public void StopKnife()
        {
            Console.WriteLine("Stopped");
            KnifeMoving = false;
        }

        private void KnifeTask()
        {
            Dictionary<string, int> coords = new Dictionary<string, int>
            {
                {"X", 3 },
                {"Y", 1 },
                {"Z", 1 },
            };

            while (!StopTask)
            {
                if (!KnifeMoving)
                {
                    Thread.Sleep(100);
                    continue;
                }

                int cur = coords[axis];
                int next = (pos) ? ++cur : --cur;
                coords[axis] = next;
                Console.WriteLine("New pos:" + coords["X"] + " " + coords["Y"] + " " + coords["Z"]);
                handler.Pos(coords["X"], coords["Y"], coords["Z"]);
                Thread.Sleep(1000);
            }
        }

        ~Stand()
        {
            StopTask = true;
        }

        private string axis;
        private bool pos;

        private Task t;
        private bool KnifeMoving;
        private bool StopTask;
    }
}
