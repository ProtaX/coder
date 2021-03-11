using System;
using System.Collections.Generic;

namespace coder.ModelBridge
{
    class ModelInstruction
    {
        private enum Type
        {
            MOVE,
            STOP,
        }

        public ModelInstruction(string axis, bool pos)
        {
            Init();
            type = Type.MOVE;
            Method = "stand.move_knife";
            Parameters.Add(axis);
            Parameters.Add(pos);
        }

        public ModelInstruction()
        {
            Init();
            type = Type.STOP;
            Method = "stand.stop_knife";
        }

        private void Init()
        {
            Parameters = new List<object>();
        }

        private Type type;
        public string Method { get; set; }
        public List<object> Parameters { get; set; }
    }
}
