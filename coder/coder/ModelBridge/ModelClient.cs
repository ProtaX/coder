using System;
using StreamJsonRpc;
using System.Net;
using System.Net.Sockets;

namespace coder.ModelBridge
{
    class ModelClient
    {
        public ModelClient(string ip, short port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            skt.Connect(ep);
            s = new NetworkStream(skt);
            jsonRpc = JsonRpc.Attach(s);
        }

        public void SendMove(string axis, bool pos)
        {
            var t = jsonRpc.InvokeAsync("stand.move_knife", axis, pos);
            t.Wait();
        }

        public void SendStop()
        {
            var t = jsonRpc.InvokeAsync("stand.stop_knife");
            t.Wait();
        }

        ~ModelClient()
        {
            s.Close();
            skt.Close();
        }

        private JsonRpc jsonRpc;
        private NetworkStream s;
        private Socket skt;
    }
}
