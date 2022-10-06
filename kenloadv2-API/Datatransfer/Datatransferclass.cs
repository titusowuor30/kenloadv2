using KenloadV2API.Data;
using KenloadV2API.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KenloadV2API.Datatransfer
{
    public class Datatransferclass : IDatatransfer
    {
        private readonly Kenloadv2Data _context;

        public Datatransferclass(Kenloadv2Data context)
        {
            _context = context;
   
        }
        
        public void DataExchange()

        {   //load connection settings
            //
            try
            {
                var connectionSettings = _context.DataExchange.FirstOrDefault();
                if(connectionSettings != null)
                {
                    Globals.connectionType = connectionSettings.connectionType;
                    Globals.port = connectionSettings.server_port;
                    Globals.server_address = connectionSettings.server_address;
                    Globals.trackerid = connectionSettings.weighbridgetickets;
                    Globals.trackertags = connectionSettings.tags;
                    if (Globals.trackerid == 0) { Globals.trackerid = 1; }
                }
             

                if (Globals.connectionType.ToLower() == "server")
                {
                    _ = syncData();
                    Thread.Sleep(1000);
                }
               



            }
            catch (Exception ex)
            {

                ex.ToString();
            }


        }
       
     
        public void DelayedJob()
        {
            Console.WriteLine("Hello from a Delayed job!");
        }
        public void FireAndForgetJob()
        {
            Console.WriteLine("Hello from a Delayed job!");
        }
    
        public void ContinuationJob()
        {
            Console.WriteLine("Hello from a Continuation job!");
        }
        public void executeClient()
        {
            try
            {

                // Establish the remote endpoint
                // for the socket. This example
                // uses port 11111 on the local
                // computer.
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddr = System.Net.IPAddress.Parse("209.50.58.236");//ipHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 5002);

                // Creation TCP/IP Socket using
                // Socket Class Constructor
                Socket sender = new Socket(ipAddr.AddressFamily,
                           SocketType.Stream, ProtocolType.Tcp);

                try
                {

                    // Connect Socket to the remote
                    // endpoint using method Connect()
                    if (!sender.Connected)
                    {
                        sender.Connect(localEndPoint);

                    }


                    // We print EndPoint information
                    // that we are connected
                    Console.WriteLine("Socket connected to -> {0} ",
                                  sender.RemoteEndPoint.ToString());

                    // Creation of message that
                    // we will send to Server

                    while (sender.Connected)
                    {

                        byte[] messageSent = Encoding.ASCII.GetBytes("Test Client<EOF>");
                        int byteSent = sender.Send(messageSent);

                        // Data buffer
                        byte[] messageReceived = new byte[1024];

                        // We receive the message using
                        // the method Receive(). This
                        // method returns number of bytes
                        // received, that we'll use to
                        // convert them to string
                        int byteRecv = sender.Receive(messageReceived);
                        Console.WriteLine("Message from Server -> {0}",
                              Encoding.ASCII.GetString(messageReceived, 0, byteRecv));
                        Thread.Sleep(1000);


                    }


                    // Close Socket using
                    // the method Close()
                    /* sender.Shutdown(SocketShutdown.Both);
                     sender.Close();*/
                }

                // Manage of Socket's Exceptions
                catch (ArgumentNullException ane)
                {

                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }

                catch (SocketException se)
                {

                    Console.WriteLine("SocketException : {0}", se.ToString());
                }

                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }

            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }

        }

        public async Task syncData()
        {
           
            var json = JsonConvert.SerializeObject(new
            {
                email = "admin@admin.com",
                password = "@Admin123"
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://209.50.58.236:4445/api/authmanagement/login";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            var result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);

            tokenDetails tokendetail = JsonConvert.DeserializeObject<tokenDetails>(result);
            Console.WriteLine(tokendetail.token);
            //record Person(string Name, string Occupation);
            // contributors.ForEach(contributor => Console.WriteLine(contributor.token));

            //post with token headers
            //load data 


            var data3 = _context.WeighbridgeTransactions.First(b => b.id == Globals.trackerid);

            
            var json2 = JsonConvert.SerializeObject(new
            {
                id = 0,
                wbrg_ticket_no =data3.wbrg_ticket_no,
                wbrg_ticket_type = data3.wbrg_ticket_type,
                wbrg_station = data3.wbrg_station,
                wbrg_roadauthority = data3.wbrg_roadauthority,
                wbrg_ticket_weighbridge = data3.wbrg_ticket_weighbridge,
                wbrg_ticket_source = data3.wbrg_ticket_source,
                wbrg_ticket_destination =data3.wbrg_ticket_destination,
                wbrg_ticket_date = data3.wbrg_ticket_date,
                wbrg_ticket_dateout =data3.wbrg_ticket_dateout,
                wbrg_product = data3.wbrg_product,
                wbrg_tx_code = data3.wbrg_tx_code,
                veh_reg = data3.veh_reg,
                anpr_veh_reg = data3.anpr_veh_reg,
                axle1_weight = data3.axle1_weight,
                axle1_limit = data3.axle1_limit,
                axle1_time = data3.axle1_time,
                axle1_group_type1 = data3.axle1_group_type1,
                axle1_group_type2 = data3.axle1_group_type2,
                axle2_weight = data3.axle2_weight,
                axle2_limit = data3.axle2_limit,
                axle2_time = data3.axle2_time,
                axle2_group_type1 = data3.axle2_group_type1,
                axle2_group_type2 = data3.axle2_group_type2,
                axle3_weight = data3.axle3_weight,
                axle3_limit = data3.axle3_limit,
                axle3_time = data3.axle3_time,
                axle3_group_type1 =data3.axle3_group_type1,
                axle3_group_type2 = data3.axle3_group_type2,
                axle4_weight = data3.axle4_weight,
                axle4_limit = data3.axle4_limit,
                axle4_time = data3.axle4_time,
                axle4_group_type1 = data3.axle4_group_type1,
                axle4_group_type2 = data3.axle4_group_type2,
                axle5_weight =data3.axle5_weight,
                axle5_limit = data3.axle5_limit,
                axle5_time = data3.axle5_time,
                axle5_group_type1 = data3.axle5_group_type1,
                axle5_group_type2 = data3.axle5_group_type2,
                axle6_weight = data3.axle6_weight,
                axle6_limit =data3.axle6_limit,
                axle6_time = data3.axle6_time,
                axle6_group_type1 =data3.axle6_group_type1,
                axle6_group_type2 = data3.axle6_group_type2,
                axle7_weight =data3.axle7_weight,
                axle7_limit = data3.axle7_limit,
                axle7_time = data3.axle7_time,
                axle7_group_type1 = data3.axle7_group_type1,
                axle7_group_type2 =data3.axle7_group_type2,
                axle8_weight =data3.axle8_weight,
                axle8_limit = data3.axle8_limit,
                axle8_time =data3.axle8_time,
                axle8_group_type1 = data3.axle8_group_type1,
                axle8_group_type2 =data3.axle8_group_type2,
                wbrg_ticket_grossweight = 0,
                wbrg_ticket_tareweight = 0,
                wbrg_ticket_stamp = 0,
                wbrg_ticket_operator = data3.wbrg_ticket_operator,
                wbrg_ticket_status = data3.wbrg_ticket_status,
                wbrg_ticket_state = data3.wbrg_ticket_state,
                wbrg_ticket_printed = data3.wbrg_ticket_printed,
                wbrg_ticket_direction = data3.wbrg_ticket_direction,
                wbrg_ticket_directionarea =data3.wbrg_ticket_directionarea,
                wbrg_ticket_axel = data3.wbrg_ticket_axel,
                wbrg_ticket_ref = data3.wbrg_ticket_ref,
                wbrg_ticket_excessweight = 0,
                wbrg_ticket_driver =data3.wbrg_ticket_driver,
                wbrg_ticket_shift = data3.wbrg_ticket_shift,
                wbrg_ticket_wim = data3.wbrg_ticket_wim,
                wbrg_ticket_notes = data3.wbrg_ticket_notes,
                wbgr_ticket_trailer_no = data3.wbgr_ticket_trailer_no,
                wbrg_ticket_excessload = data3.wbrg_ticket_excessload,
                yardlistid = data3.yardlistid,
                wbrg_ticket_case_id = data3.wbrg_ticket_case_id,
                wbrg_ticket_reweigh_no = data3.wbrg_ticket_reweigh_no,
                wbrg_ticket_tagged = data3.wbrg_ticket_tagged,
                wbrg_ticket_charge_no =data3.wbrg_ticket_charge_no,
                wbrg_ticket_axleload = data3.wbrg_ticket_axleload,
                wbrg_ticket_gvwload = data3.wbrg_ticket_gvwload,
                permit_no = data3.permit_no,
                wbrg_ticket_make = data3.axle5_limit,
                yardList = ""
            });
            var data2 = new StringContent(json2, Encoding.UTF8, "application/json");

            var url2 = "http://209.50.58.236:4445/api/weighbridgetransactions";
            using var client2 = new HttpClient();
            client2.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokendetail.token);
            try
            {
                var response2 = await client2.PostAsync(url2, data2);
                var result2 = response2.Content.ReadAsStringAsync().Result;
                var updateDataTransfer = _context.DataExchange.Where(item => item.weighbridgetickets == Globals.trackerid).FirstOrDefault();
                updateDataTransfer.weighbridgetickets = Globals.trackerid + 1;
                _context.SaveChanges();
            }
          catch (Exception ex)
            {
                 ex.ToString();
            }
           

          
        }
        public class tokenDetails
        {

            public string token { get; set; }

            public string success { get; set; }

            public string errors { get; set; }

        }

    }
}
