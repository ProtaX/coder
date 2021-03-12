using System;
using StreamJsonRpc;
using System.Net;
using System.Net.Sockets;
using System.Net.Http;
using System.Collections.Generic;
using System.IO;

namespace stand
{
    class GUIClient
    {
        public GUIClient(string ip, short port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            skt.Connect(ep);
            s = new NetworkStream(skt);
            jsonRpc = JsonRpc.Attach(s);
        }

        public void SendCoords(int x, int y, int z)
        {
            var t = jsonRpc.InvokeAsync("display.set_knife_position", x, y, z);
            t.Wait();
        }

        ~GUIClient()
        {
            s.Close();
            skt.Close();
        }

        private string url;
        private JsonRpc jsonRpc;
        private NetworkStream s;
        private Socket skt;
    }
}
