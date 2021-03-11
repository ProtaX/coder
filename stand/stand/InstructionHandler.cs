using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stand
{
    class InstructionHandler
    {
        public delegate void PosEvent(int x, int y, int z);

        public event PosEvent SendPos;

        public void Pos(int x, int y, int z)
        {
            SendPos?.Invoke(x, y, z);
        }
    }
}
