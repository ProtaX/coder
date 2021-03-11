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

        }

        [JsonRpcMethod("stand.stop_knife")]
        public void StopKnife()
        {

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
                    continue;
                }

                int cur = coords[axis];
                int next = (pos) ? ++cur : --cur;
                coords[axis] = next;


            }
        }

        private string axis;
        private bool pos;

        private Task t;
        private bool KnifeMoving;
        private bool StopTask;
    }
}
