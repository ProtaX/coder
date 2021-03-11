using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;

using StreamJsonRpc;

namespace display
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int knifeCurX = 0;
        public int knifeCurY = 0;
        public int knifeCurZ = 0;

        public MainWindow()
        {
            InitializeComponent();
            Task.Run(start_listening);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse("192.168.0.100"), 6666);
                Socket skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                skt.Connect(ip);

                NetworkStream s = new NetworkStream(skt);
                var jsonrpc = JsonRpc.Attach(s);

                var t =  jsonrpc.InvokeAsync("coder.press_key", "YMAX");
                t.Wait();

                skt.Close();
            } catch (Exception ex)
            {
                Console.WriteLine("Cannot connect to coder: " + ex.Message);
                return;
            }
        }
        private void start_listening()
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 4000);
            Socket skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            skt.Bind(ip);
            skt.Listen(1);

            while (true)
            {
                var clientSkt = skt.Accept();
                NetworkStream s = new NetworkStream(clientSkt);
                var jsonRpc = JsonRpc.Attach(s);

                jsonRpc.AllowModificationWhileListening = true;
                jsonRpc.AddLocalRpcMethod("display.set_knife_position", new Action<int, int, int>(
                    (x, y, z) => {
                        Console.WriteLine("Received = " + x + ", " + y + ", " + z);
                        MoveKnife(x, y, z);
                        

                        return;
                    }
                ));
                jsonRpc.AllowModificationWhileListening = false;
            }

        }

        private void MoveKnife(int newX, int newY, int newZ)
        {
            this.Dispatcher.Invoke(() =>
            {
                const int pxPerUnit = 2;

                knife_from_side.RenderTransform = new TranslateTransform(
                    (newX - knifeCurX) * pxPerUnit,
                    (newZ - knifeCurZ) * pxPerUnit
                );
                knife_from_top.RenderTransform = new TranslateTransform(
                    (newX - knifeCurX) * pxPerUnit,
                    (newY - knifeCurY) * pxPerUnit
                );

                myGrid.InvalidateVisual();

            });
            //knife.RenderTransform.Value.Translate(newX, newY);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse("192.168.0.100"), 6666);
                Socket skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                skt.Connect(ip);

                NetworkStream s = new NetworkStream(skt);
                var jsonrpc = JsonRpc.Attach(s);

                var t = jsonrpc.InvokeAsync("coder.press_key", "Manual");
                t.Wait();

                skt.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot connect to coder: " + ex.Message);
                return;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse("192.168.0.100"), 6666);
                Socket skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                skt.Connect(ip);

                NetworkStream s = new NetworkStream(skt);
                var jsonrpc = JsonRpc.Attach(s);

                var t = jsonrpc.InvokeAsync("coder.press_key", "Auto");
                t.Wait();

                skt.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot connect to coder: " + ex.Message);
                return;
            }
        }


        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse("192.168.0.100"), 6666);
                Socket skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                skt.Connect(ip);

                NetworkStream s = new NetworkStream(skt);
                var jsonrpc = JsonRpc.Attach(s);

                var t = jsonrpc.InvokeAsync("coder.press_key", "Step");
                t.Wait();

                skt.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot connect to coder: " + ex.Message);
                return;
            }

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse("192.168.0.100"), 6666);
                Socket skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                skt.Connect(ip);

                NetworkStream s = new NetworkStream(skt);
                var jsonrpc = JsonRpc.Attach(s);

                var t = jsonrpc.InvokeAsync("coder.press_key", "Stop");
                t.Wait();

                skt.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot connect to coder: " + ex.Message);
                return;
            }

        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse("192.168.0.100"), 6666);
                Socket skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                skt.Connect(ip);

                NetworkStream s = new NetworkStream(skt);
                var jsonrpc = JsonRpc.Attach(s);

                var t = jsonrpc.InvokeAsync("coder.press_key", "End");
                t.Wait();
                skt.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot connect to coder: " + ex.Message);
                return;
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse("192.168.0.100"), 6666);
                Socket skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                skt.Connect(ip);

                NetworkStream s = new NetworkStream(skt);
                var jsonrpc = JsonRpc.Attach(s);

                var t = jsonrpc.InvokeAsync("coder.set_parameter", "XMAX", XMAX.Text);
                t.Wait();
                skt.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot connect to coder: " + ex.Message);
                return;
            }
                }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
          
            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse("192.168.0.100"), 6666);
                Socket skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                skt.Connect(ip);

                NetworkStream s = new NetworkStream(skt);
                var jsonrpc = JsonRpc.Attach(s);

                var t = jsonrpc.InvokeAsync("coder.set_parameter", "YMAX", YMAX.Text);
                t.Wait();

                skt.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot connect to coder: " + ex.Message);
                return;
            }
        }

        private void ZMAX_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse("192.168.0.100"), 6666);
                Socket skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                skt.Connect(ip);

                NetworkStream s = new NetworkStream(skt);
                var jsonrpc = JsonRpc.Attach(s);

                var t = jsonrpc.InvokeAsync("coder.set_parameter", "ZMAX", ZMAX.Text);
                t.Wait();

                skt.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot connect to coder: " + ex.Message);
                return;
            }
        }

        private void TZAD_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse("192.168.0.100"), 6666);
                Socket skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                skt.Connect(ip);

                NetworkStream s = new NetworkStream(skt);
                var jsonrpc = JsonRpc.Attach(s);

                var t = jsonrpc.InvokeAsync("coder.set_parameter", "TZAD", TZAD.Text);
                t.Wait();

                skt.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot connect to coder: " + ex.Message);
                return;
            }
        }
    }
}
