using System;
using System.IO;

using coder.GUIBridge;
using coder.ModelBridge;

namespace coder
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Error: no program file provided");
                Environment.Exit(-1);
            }
            string progPath = args[0];
            string progCode = "";

            try
            {
                progCode = File.ReadAllText(progPath);
                Console.WriteLine("Program code read");
            } catch (Exception ex)
            {
                Console.WriteLine("Error: cannot read program code from " + progPath + " : " + ex.Message);
                Environment.Exit(-1);
            }

            Console.WriteLine("Press any key to connect to stand...");
            Console.ReadKey();
            var modelClient = new ModelClient("192.168.1.126", 8080);
            var instHandler = new InstructionHandler();
            instHandler.SendMove += modelClient.SendMove;
            instHandler.SendStop += modelClient.SendStop;

            Console.WriteLine("Connected to stand, waiting for client...");
            var rpcAccepter = new Coder(progCode, instHandler);
            new GUIListener().Accept(rpcAccepter);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
