using System;
using StreamJsonRpc;
using System.Net;
using System.Net.Sockets;

namespace coder.GUIBridge
{
    class GUIListener
    {
        public GUIListener()
        {
            try
            {
                listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6666);
                listener.Bind(ip);
                listener.Listen(1);
            } catch (Exception ex)
            {
                Console.WriteLine("Cannon create GUIListener: " + ex.Message);
                Environment.Exit(-1);
            }   
        }

        public void Accept(object accepter)
        {
            try
            {
                var client = listener.Accept();
                Console.WriteLine("Client accepted");
                clientStream = new NetworkStream(client);
                jsonrpc = JsonRpc.Attach(clientStream, accepter);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot accept connection from GUI: " + ex.Message);
                Environment.Exit(-1);
            }
        }

        ~GUIListener() {
            clientStream.Close();
            listener.Close();
        }

        Socket listener;
        NetworkStream clientStream;
        private JsonRpc jsonrpc;
    }
}
