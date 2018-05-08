using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WeatherReciever
{
    /// <summary>
    /// Modtager Data fra Pi
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            UdpClient udpServer = new UdpClient(9877);
            string _recievedData;

        //Creates an IPEndPoint to record the IP Address and port number of the sender.  
        IPAddress ip = IPAddress.Parse("192.168.6.145");
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(ip, 9877);

            try
            {
                // Blocks until a message is received on this socket from a remote host (a client).
                Console.WriteLine("Server is Online");
                while (true)
                {
                    Byte[] receiveBytes = udpServer.Receive(ref RemoteIpEndPoint);
                    //Server is now activated");

                    string receivedData = Encoding.ASCII.GetString(receiveBytes);
                    _recievedData = receivedData;

                    Console.WriteLine(receivedData);
                    //Console.WriteLine("Received from: " + clientName.ToString() + " " + text.ToString());

                
                  
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        
    }

        public void ToDataBase(string temp)
        {
            
        }
    }
}
