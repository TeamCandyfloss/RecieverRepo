using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;

namespace WeatherReciever
{
    /// <summary>
    ///     Modtager Data fra Pi
    /// </summary>
    public class Program
    {

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
                    string temp;
                    var receiveBytes = udpServer.Receive(ref RemoteIpEndPoint);
                    //Server is now activated");
                    Console.WriteLine("Recieved Data");
                    var receivedData = Encoding.ASCII.GetString(receiveBytes);
                    string[] s = receivedData.Split();
                    temp = s[0];
                    Console.WriteLine("data processed");
                    Console.WriteLine("Recorded temperature: "+receivedData);

                    pm.ToDataBase(receivedData);

                
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public async void ToDataBase(string temp)
        {
            //var time = DateTime.Now.ToShortTimeString();
            //var place = "Roskilde";
            HttpClient client = new HttpClient();

            //if (place == null || time == null || temp == null)
            //{
            //    throw new ArgumentException("Du kan ikke indsætte en Null værdi");
            //}

            //var _connectionString = "Server=tcp:3semesterxxx.database.windows.net,1433;Initial Catalog=WCFSTUDENT;Persist Security Info=False;User ID=Admeme;Password=Skole123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    var sqlQuery =
            //        "INSERT INTO Temperature (Temperature, Time, Place) VALUES (@Temperature, @Time, @Place)";

            //    using (var command = new SqlCommand(sqlQuery, connection))
            //    {
            //        command.Parameters.AddWithValue("@Temperature", temp);
            //        command.Parameters.AddWithValue("@Time", time);
            //        command.Parameters.AddWithValue("@Place", place);

            //        connection.Open();
            //        var result = command.ExecuteNonQuery();
            //        if (result < 0) throw new ArgumentException("Nothing has been added to the Database");
            //        return result;
            //        // tjekker for fejl i indsættelsen skriver hvis der er fejl
            //    }
            //}

           
            HttpContent encodedTemp = new StringContent(temp);
            HttpContent s = new StringContent(temp,Encoding.UTF8, "application/json");
            

            
            var response = await client.PostAsync("http://pleaseworknow.azurewebsites.net/service1.svc/temperatures/PostTemp", s);
            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"{response.StatusCode} + {response.IsSuccessStatusCode}");
        }
    }
}