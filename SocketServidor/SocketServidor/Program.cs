using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SocketServidor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            server();

        }
        public static void server()
        {
            //CONFI SERVER
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11200);
            try
            {
                //CREACION DE LOS SOCKETS
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(localEndPoint);
                listener.Listen(10);
                Console.WriteLine("Esperando Conexion");
                Socket handler = listener.Accept();

                string data = null;
                byte[] bytes = null;
                while (true)
                {
                    bytes = new byte[1024];
                    int byteRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, byteRec);
                    if (data.IndexOf("<EOF>") > -1)
                        break;
                    Console.WriteLine("Texto del Cliente: " + data.Replace("<EOF>",""));
                   
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
        }

    }
}
