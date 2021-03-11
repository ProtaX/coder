using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stand
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to connect to GUI...");
            Console.ReadKey();
            var guiClient = new GUIClient("127.0.0.1", 4000);
            var instHandler = new InstructionHandler();
            instHandler.SendPos += guiClient.SendCoords;

            Console.WriteLine("Connected to GUI, waiting for client...");
            var rpcAccepter = new Stand(instHandler);
            new CoderListener().Accept(rpcAccepter);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
