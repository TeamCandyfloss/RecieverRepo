using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WeatherReciever
{
    /// <summary>
    ///     Modtager Data fra Pi
    /// </summary>
    public class Program
    {
        public int QQ = 0;

        public static void Main(string[] args)
        {
            var pm = new Program();
            var udpServer = new UdpClient(9877);

            //Creates an IPEndPoint to record the IP Address and port number of the sender.  
            var ip = IPAddress.Parse("192.168.6.145");
            var RemoteIpEndPoint = new IPEndPoint(ip, 9877);


            
            try
            {
                // Blocks until a message is received on this socket from a remote host (a client).
                Console.WriteLine("Server is Online");

                while (true)
                {
                    var receiveBytes = udpServer.Receive(ref RemoteIpEndPoint);
                    //Server is now activated");

                    var receivedData = Encoding.ASCII.GetString(receiveBytes);

                    Console.WriteLine(receivedData);
                    //Console.WriteLine("Received from: " + clientName.ToString() + " " + text.ToString());

                    pm.ToDataBase(receivedData);

                    if (pm.QQ > 3)
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void ToDataBase(string temp)
        {
            var time = DateTime.Now.ToShortTimeString();
            var place = "Roskilde";

            var _connectionString = "Server=tcp:3semesterxxx.database.windows.net,1433;Initial Catalog=WCFSTUDENT;Persist Security Info=False;User ID=Admeme;Password=Skole123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery =
                    "INSERT INTO Temperature (Temperature, Time, Place) VALUES (@Temperature, @Time, @Place)";

                using (var command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Temperature", temp);
                    command.Parameters.AddWithValue("@Time", time);
                    command.Parameters.AddWithValue("@Place", place);

                    connection.Open();
                    var result = command.ExecuteNonQuery();

                    if (result < 0) Console.WriteLine("Error inserting data into the database.");
                    QQ++;
                }
            }
        }
    }
}