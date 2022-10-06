using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace kenloadv2wms
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _ = syncWeighbridgeTickets();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
        public static async Task syncWeighbridgeTickets()
        {
            string clientUrl = "http://127.0.0.1:4444/";
            string clientemail = "admin@admin.com";
            string clientpassword = "@Admin123";
            string serverUrl = "http://212.49.81.13:4444/";
            string serveremail = "admin@admin.com";
            string serverpassword = "@Admin123";
            string platformType = "client";
            int weighbridgeid = 0;
            string serverToken = "";
            string clientToken = "";
            string imageFolderPath = "C:\\xampp\\htdocs\\kenload\\dashboard\\imgs\\";
            // string imageFolderClient = "F:\\kenload\\htdocs\\kenload\\dashboard\\imgs\\";
            string imageFolderClient = "E:\\Kenloadimg\\kenload\\dashboard\\imgs\\";
            while (true)
            {
                {

                    //get local token 
                    clientToken = await getToken(clientemail, clientpassword, clientUrl);
                    //get local details
                   // DataExchange exchangeDetails = await getConnectionDetails(clientemail, clientpassword, clientUrl, clientToken);
                    serverToken = await getToken(serveremail, serverpassword, serverUrl);

                    if (serverToken != null)
                    {
                       // int tracker = exchangeDetails.weighbridgetickets;
                        //int tagsTracker = exchangeDetails.tags;
                        // platformType = exchangeDetails.connectionType;
                        // string serverUrl = exchangeDetails.server_address;
                        //clientUrl = exchangeDetails.client_address;
                        //string serveremail = exchangeDetails.server_email;
                        //clientemail = exchangeDetails.client_address;
                        // string serverpassword = DecryptCipherTextToPlainText(exchangeDetails.server_password);
                        //clientpassword = DecryptCipherTextToPlainText(exchangeDetails.client_password);
                        //get server token
                        // string serverToken = await getToken(serveremail, serverpassword, serverUrl);
                        if (platformType.ToLower() != "client")
                        {
                            return;
                        }
                       // if (tracker == 0) { tracker = 1; }
                       // if (tagsTracker == 0) { tagsTracker = 1; }

                        //get local data from endpoint
                        try
                        {
                            var json4 = JsonConvert.SerializeObject(new
                            {
                                email = clientemail,
                                password = clientpassword
                            });
                            var data4 = new StringContent(json4, Encoding.UTF8, "application/json");
                            //var url4 = clientUrl + "api/weighbridgetransactions/67";
                            var url4 = clientUrl + "api/weighbridgetransactions/search?exported=0&status=O,W,L,C,X&limit=1&asc=asc";
                            using var client4 = new HttpClient();
                            client4.DefaultRequestHeaders.Add("Authorization", "Bearer " + clientToken);
                            var response4 = await client4.GetAsync(url4);
                            var result4 = response4.Content.ReadAsStringAsync().Result;


                            List<WeighbridgeTransactions> transactions2 = JsonConvert.DeserializeObject<List<WeighbridgeTransactions>>(result4);
                            // Console.WriteLine(result4);
                            WeighbridgeTransactions transactions = new WeighbridgeTransactions();
                            foreach(WeighbridgeTransactions tx in transactions2)
                                transactions=tx;

                            //post data to server endpoint
                            if (transactions.wbrg_ticket_no != null)
                            {
                                try
                                {
                                    var json2 = JsonConvert.SerializeObject(new
                                    {
                                        wbrg_ticket_no = transactions.wbrg_ticket_no,
                                        wbrg_ticket_type = transactions.wbrg_ticket_type,
                                        wbrg_station = transactions.wbrg_station,
                                        wbrg_roadauthority = transactions.wbrg_roadauthority,
                                        wbrg_ticket_weighbridge = transactions.wbrg_ticket_weighbridge,
                                        wbrg_ticket_source = transactions.wbrg_ticket_source,
                                        wbrg_ticket_destination = transactions.wbrg_ticket_destination,
                                        wbrg_ticket_date = transactions.wbrg_ticket_date,
                                        wbrg_ticket_dateout = transactions.wbrg_ticket_dateout,
                                        wbrg_product = transactions.wbrg_product,
                                        wbrg_tx_code = transactions.wbrg_tx_code,
                                        veh_reg = transactions.veh_reg,
                                        anpr_veh_reg = transactions.anpr_veh_reg,
                                        axle1_weight = transactions.axle1_weight,
                                        axle1_limit = transactions.axle1_limit,
                                        axle1_time = transactions.axle1_time,
                                        axle1_group_type1 = transactions.axle1_group_type1,
                                        axle1_group_type2 = transactions.axle1_group_type2,
                                        axle2_weight = transactions.axle2_weight,
                                        axle2_limit = transactions.axle2_limit,
                                        axle2_time = transactions.axle2_time,
                                        axle2_group_type1 = transactions.axle2_group_type1,
                                        axle2_group_type2 = transactions.axle2_group_type2,
                                        axle3_weight = transactions.axle3_weight,
                                        axle3_limit = transactions.axle3_limit,
                                        axle3_time = transactions.axle3_time,
                                        axle3_group_type1 = transactions.axle3_group_type1,
                                        axle3_group_type2 = transactions.axle3_group_type2,
                                        axle4_weight = transactions.axle4_weight,
                                        axle4_limit = transactions.axle4_limit,
                                        axle4_time = transactions.axle4_time,
                                        axle4_group_type1 = transactions.axle4_group_type1,
                                        axle4_group_type2 = transactions.axle4_group_type2,
                                        axle5_weight = transactions.axle5_weight,
                                        axle5_limit = transactions.axle5_limit,
                                        axle5_time = transactions.axle5_time,
                                        axle5_group_type1 = transactions.axle5_group_type1,
                                        axle5_group_type2 = transactions.axle5_group_type2,
                                        axle6_weight = transactions.axle6_weight,
                                        axle6_limit = transactions.axle6_limit,
                                        axle6_time = transactions.axle6_time,
                                        axle6_group_type1 = transactions.axle6_group_type1,
                                        axle6_group_type2 = transactions.axle6_group_type2,
                                        axle7_weight = transactions.axle7_weight,
                                        axle7_limit = transactions.axle7_limit,
                                        axle7_time = transactions.axle7_time,
                                        axle7_group_type1 = transactions.axle7_group_type1,
                                        axle7_group_type2 = transactions.axle7_group_type2,
                                        axle8_weight = transactions.axle8_weight,
                                        axle8_limit = transactions.axle8_limit,
                                        axle8_time = transactions.axle8_time,
                                        axle8_group_type1 = transactions.axle8_group_type1,
                                        axle8_group_type2 = transactions.axle8_group_type2,
                                        wbrg_ticket_grossweight = transactions.wbrg_ticket_grossweight,
                                        wbrg_ticket_tareweight = transactions.wbrg_ticket_tareweight,
                                        wbrg_ticket_stamp = transactions.wbrg_ticket_stamp,
                                        wbrg_ticket_operator = transactions.wbrg_ticket_operator,
                                        wbrg_ticket_status = transactions.wbrg_ticket_status,
                                        wbrg_ticket_state = transactions.wbrg_ticket_state,
                                        wbrg_ticket_printed = transactions.wbrg_ticket_printed,
                                        wbrg_ticket_direction = transactions.wbrg_ticket_direction,
                                        wbrg_ticket_directionarea = transactions.wbrg_ticket_directionarea,
                                        wbrg_ticket_axel = transactions.wbrg_ticket_axel,
                                        wbrg_ticket_ref = transactions.wbrg_ticket_ref,
                                        wbrg_ticket_excessweight = transactions.wbrg_ticket_excessweight,
                                        wbrg_ticket_driver = transactions.wbrg_ticket_driver,
                                        wbrg_ticket_shift = transactions.wbrg_ticket_shift,
                                        wbrg_ticket_wim = transactions.wbrg_ticket_wim,
                                        wbrg_ticket_notes = transactions.wbrg_ticket_notes,
                                        wbgr_ticket_trailer_no = transactions.wbgr_ticket_trailer_no,
                                        wbrg_ticket_excessload = transactions.wbrg_ticket_excessload,
                                        yardlistid = transactions.yardlistid,
                                        wbrg_ticket_case_id = transactions.wbrg_ticket_case_id,
                                        wbrg_ticket_reweigh_no = transactions.wbrg_ticket_reweigh_no,
                                        wbrg_ticket_tagged = transactions.wbrg_ticket_tagged,
                                        wbrg_ticket_charge_no = transactions.wbrg_ticket_charge_no,
                                        wbrg_ticket_axleload = transactions.wbrg_ticket_axleload,
                                        wbrg_ticket_gvwload = transactions.wbrg_ticket_gvwload,
                                        permit_no = transactions.permit_no,
                                        wbrg_ticket_make = transactions.wbrg_ticket_make,
                                        exported = transactions.exported,
                                        reliefvehreg =transactions.reliefvehreg,
                                        yardList = transactions.yardList
                                    });

                                    var data2 = new StringContent(json2, Encoding.UTF8, "application/json");
                                    var url2 = serverUrl + "api/weighbridgetransactions";
                                    using var client2 = new HttpClient();
                                    client2.DefaultRequestHeaders.Add("Authorization", "Bearer " + serverToken);
                                    var response2 = await client2.PostAsync(url2, data2);
                                    var result2 = response2.Content.ReadAsStringAsync().Result;

                                    WeighbridgeTransactions transactionsReturn = JsonConvert.DeserializeObject<WeighbridgeTransactions>(result2);
                                    weighbridgeid = transactionsReturn.id;
                                }
                                catch (Exception e)
                                {
                                    e.ToString();
                                }
                                //upload axles--pending
                                //get axles
                                try
                                {
                                    var json12 = JsonConvert.SerializeObject(new
                                    {
                                        email = clientemail,
                                        password = clientpassword
                                    });
                                    var data12 = new StringContent(json4, Encoding.UTF8, "application/json");
                                    var url12 = clientUrl + "api/AxleWeights/search?wbtno=" + transactions.id;
                                    using var client12 = new HttpClient();
                                    client12.DefaultRequestHeaders.Add("Authorization", "Bearer " + clientToken);
                                    var response12 = await client12.GetAsync(url12);
                                    var result12 = response12.Content.ReadAsStringAsync().Result;

                                    List<AxleWeight> axleweights = JsonConvert.DeserializeObject<List<AxleWeight>>(result12);

                                    foreach (var axleweight in axleweights)
                                    {
                                        var json13 = JsonConvert.SerializeObject(new
                                        {
                                            id = axleweight.id,
                                            wbrg_ticket_no = axleweight.wbrg_ticket_no,
                                            axle_actualweight = axleweight.axle_actualweight,
                                            axle_permissibleweight = axleweight.axle_permissibleweight,
                                            axle_variantweight = axleweight.axle_variantweight,
                                            axle_time = axleweight.axle_time,
                                            axle_group = axleweight.axle_group,
                                            axle_grouping = axleweight.axle_grouping,
                                            timestamp = axleweight.timestamp,
                                            axle_number = axleweight.axle_number,
                                            fee = axleweight.fee,
                                            weighbridgetransactionsid = weighbridgeid

                                        });

                                        var data13 = new StringContent(json13, Encoding.UTF8, "application/json");
                                        var url13 = serverUrl + "api/AxleWeight";
                                        using var client13 = new HttpClient();
                                        client13.DefaultRequestHeaders.Add("Authorization", "Bearer " + serverToken);
                                        var response13 = await client13.PostAsync(url13, data13);
                                        var result13 = response13.Content.ReadAsStringAsync().Result;
                                    }
                                }
                                catch (Exception e)
                                {
                                    e.ToString();
                                }
                                try
                                {
                                    var hour = transactions.wbrg_ticket_date.Hour.ToString();
                                    var year = transactions.wbrg_ticket_date.Year.ToString();
                                    var month = transactions.wbrg_ticket_date.Month.ToString();
                                    if (month.Length < 2) { month = "0" + month; }
                                    if (hour.Length < 2) { hour = "0" + hour; }
                                    var day = transactions.wbrg_ticket_date.Day.ToString();
                                    if (day.Length < 2) { day = "0" + day; }
                                    var dir = year + month + "/" + day + "/" + hour + "/";
                                    //Console.WriteLine("Ticket:"+transactions.wbrg_ticket_date);
                                    //FileStream fs = File.OpenRead("C:\\xampp\\htdocs\\kenload\\dashboard\\imgs\\"+dir+"F"+transactions.wbrg_ticket_no+".jpg");
                                    string[] imagePrefix = { "F", "O", "S" };
                                    foreach (var image in imagePrefix)
                                    {
                                        try { 
                                        
                                            var filePath = imageFolderClient + dir + image + transactions.wbrg_ticket_no + ".jpg";
                                            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                                            var filePath2 = imageFolderPath + dir;
                                            _ = UploadImages(serverUrl + "api/UploadFile/uploadSingle?folderPath=" + filePath2 + "&prefix=" + image, fileStream, transactions.wbrg_ticket_no);

                                        }
                                        catch (Exception ex)
                                        {
                                            ex.ToString();
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    e.ToString();
                                }
                                //update weighbridge transactions table


                                try
                                {
                                    var json8 = JsonConvert.SerializeObject(new
                                    {
                                        //add id here
                                        id=transactions.id,
                                        wbrg_ticket_no = transactions.wbrg_ticket_no,
                                        wbrg_ticket_type = transactions.wbrg_ticket_type,
                                        wbrg_station = transactions.wbrg_station,
                                        wbrg_roadauthority = transactions.wbrg_roadauthority,
                                        wbrg_ticket_weighbridge = transactions.wbrg_ticket_weighbridge,
                                        wbrg_ticket_source = transactions.wbrg_ticket_source,
                                        wbrg_ticket_destination = transactions.wbrg_ticket_destination,
                                        wbrg_ticket_date = transactions.wbrg_ticket_date,
                                        wbrg_ticket_dateout = transactions.wbrg_ticket_dateout,
                                        wbrg_product = transactions.wbrg_product,
                                        wbrg_tx_code = transactions.wbrg_tx_code,
                                        veh_reg = transactions.veh_reg,
                                        anpr_veh_reg = transactions.anpr_veh_reg,   
                                        axle1_weight = transactions.axle1_weight,
                                        axle1_limit = transactions.axle1_limit,
                                        axle1_time = transactions.axle1_time,
                                        axle1_group_type1 = transactions.axle1_group_type1,
                                        axle1_group_type2 = transactions.axle1_group_type2,
                                        axle2_weight = transactions.axle2_weight,
                                        axle2_limit = transactions.axle2_limit,
                                        axle2_time = transactions.axle2_time,
                                        axle2_group_type1 = transactions.axle2_group_type1,
                                        axle2_group_type2 = transactions.axle2_group_type2,
                                        axle3_weight = transactions.axle3_weight,
                                        axle3_limit = transactions.axle3_limit,
                                        axle3_time = transactions.axle3_time,
                                        axle3_group_type1 = transactions.axle3_group_type1,
                                        axle3_group_type2 = transactions.axle3_group_type2,
                                        axle4_weight = transactions.axle4_weight,
                                        axle4_limit = transactions.axle4_limit,
                                        axle4_time = transactions.axle4_time,
                                        axle4_group_type1 = transactions.axle4_group_type1,
                                        axle4_group_type2 = transactions.axle4_group_type2,
                                        axle5_weight = transactions.axle5_weight,
                                        axle5_limit = transactions.axle5_limit,
                                        axle5_time = transactions.axle5_time,
                                        axle5_group_type1 = transactions.axle5_group_type1,
                                        axle5_group_type2 = transactions.axle5_group_type2,
                                        axle6_weight = transactions.axle6_weight,
                                        axle6_limit = transactions.axle6_limit,
                                        axle6_time = transactions.axle6_time,
                                        axle6_group_type1 = transactions.axle6_group_type1,
                                        axle6_group_type2 = transactions.axle6_group_type2,
                                        axle7_weight = transactions.axle7_weight,
                                        axle7_limit = transactions.axle7_limit,
                                        axle7_time = transactions.axle7_time,
                                        axle7_group_type1 = transactions.axle7_group_type1,
                                        axle7_group_type2 = transactions.axle7_group_type2,
                                        axle8_weight = transactions.axle8_weight,
                                        axle8_limit = transactions.axle8_limit,
                                        axle8_time = transactions.axle8_time,
                                        axle8_group_type1 = transactions.axle8_group_type1,
                                        axle8_group_type2 = transactions.axle8_group_type2,
                                        wbrg_ticket_grossweight = transactions.wbrg_ticket_grossweight,
                                        wbrg_ticket_tareweight = transactions.wbrg_ticket_tareweight,
                                        wbrg_ticket_stamp = transactions.wbrg_ticket_stamp,
                                        wbrg_ticket_operator = transactions.wbrg_ticket_operator,
                                        wbrg_ticket_status = transactions.wbrg_ticket_status,
                                        wbrg_ticket_state = transactions.wbrg_ticket_state,
                                        wbrg_ticket_printed = transactions.wbrg_ticket_printed,
                                        wbrg_ticket_direction = transactions.wbrg_ticket_direction,
                                        wbrg_ticket_directionarea = transactions.wbrg_ticket_directionarea,
                                        wbrg_ticket_axel = transactions.wbrg_ticket_axel,
                                        wbrg_ticket_ref = transactions.wbrg_ticket_ref,
                                        wbrg_ticket_excessweight = transactions.wbrg_ticket_excessweight,
                                        wbrg_ticket_driver = transactions.wbrg_ticket_driver,
                                        wbrg_ticket_shift = transactions.wbrg_ticket_shift,
                                        wbrg_ticket_wim = transactions.wbrg_ticket_wim,
                                        wbrg_ticket_notes = transactions.wbrg_ticket_notes,
                                        wbgr_ticket_trailer_no = transactions.wbgr_ticket_trailer_no,
                                        wbrg_ticket_excessload = transactions.wbrg_ticket_excessload,
                                        yardlistid = transactions.yardlistid,
                                        wbrg_ticket_case_id = transactions.wbrg_ticket_case_id,
                                        wbrg_ticket_reweigh_no = transactions.wbrg_ticket_reweigh_no,
                                        wbrg_ticket_tagged = transactions.wbrg_ticket_tagged,
                                        wbrg_ticket_charge_no = transactions.wbrg_ticket_charge_no,
                                        wbrg_ticket_axleload = transactions.wbrg_ticket_axleload,
                                        wbrg_ticket_gvwload = transactions.wbrg_ticket_gvwload,
                                        permit_no = transactions.permit_no,
                                        wbrg_ticket_make = transactions.wbrg_ticket_make,
                                        exported= 1,
                                        reliefvehreg = transactions.reliefvehreg,
                                        yardList = transactions.yardList

                                    });
                                    var data8 = new StringContent(json8, Encoding.UTF8, "application/json");
                                    var url8 = clientUrl + "api/weighbridgetransactions/"+transactions.id;
                                    using var client8 = new HttpClient();
                                    client8.DefaultRequestHeaders.Add("Authorization", "Bearer " + clientToken);
                                    var response8 = await client8.PutAsync(url8, data8);
                                    var result8 = response8.Content.ReadAsStringAsync().Result;
                                    Console.WriteLine("WeighbridgeTicket:" + transactions.wbrg_ticket_no);

                                }
                                catch (Exception e)
                                {
                                    e.ToString();
                                }


                            }
                            else
                            {
                                Console.WriteLine("finished tickets");
                            }

                        }
                        catch (Exception e)
                        {
                            e.ToString();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Send Error:Check Internet Connection");
                        Thread.Sleep(1000);
                    }


                    //  Thread.Sleep(1);
                }
            }
        }
        public static async Task<DataExchange> getConnectionDetails(string emailCon, string passCon, string urlString, string Token)
        {
            try
            {
                var json7 = JsonConvert.SerializeObject(new
                {
                    email = emailCon,
                    password = passCon
                });
                var data7 = new StringContent(json7, Encoding.UTF8, "application/json");
                var url7 = urlString + "api/dataexchange/1";
                using var client7 = new HttpClient();
                client7.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                var response7 = await client7.GetAsync(url7);
                var result7 = response7.Content.ReadAsStringAsync().Result;
                DataExchange exchangeDetails = JsonConvert.DeserializeObject<DataExchange>(result7);
                return exchangeDetails;
            }

            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }
        public static async Task<string> getToken(string email, string pass, string urlString)
        {
            try
            {
                var json = JsonConvert.SerializeObject(new
                {
                    email = email,
                    password = pass
                });
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = urlString + "api/authmanagement/login";
                using var client = new HttpClient();
                var response = await client.PostAsync(url, data);
                var result = response.Content.ReadAsStringAsync().Result;
                tokenDetails tokendetails = JsonConvert.DeserializeObject<tokenDetails>(result);
                return tokendetails.token;
            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }


        }
        static private async Task<System.IO.Stream> UploadImages(string actionUrl, Stream paramFileStream, string ticket)
        {
            // HttpContent stringContent = new StringContent(paramString);
            HttpContent fileStreamContent = new StreamContent(paramFileStream);


            // HttpContent bytesContent = new ByteArrayContent(paramFileBytes);
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                //formData.Add(stringContent, "param1", "param1");
                formData.Add(fileStreamContent, "files", ticket);
                // formData.Add(bytesContent, "file2", "file2");
                var response = await client.PostAsync(actionUrl, formData);
                if (!response.IsSuccessStatusCode)
                {
                    return null;

                }
                return await response.Content.ReadAsStreamAsync();

            }
        }
        public class tokenDetails
        {
            public string token { get; set; }
            public string success { get; set; }
            public string errors { get; set; }
        }
        public class AxleWeight
        {
            public int id { get; set; }
            public string wbrg_ticket_no { get; set; }
            public double axle_actualweight { get; set; }
            public double axle_permissibleweight { get; set; }
            public double axle_variantweight { get; set; }
            public DateTime axle_time { get; set; }
            public string axle_group { get; set; }
            public string axle_grouping { get; set; }
            public DateTime timestamp { get; set; }
            public int axle_number { get; set; }
            public double fee { get; set; }
            public int weighbridgetransactionsid { get; set; }
        }
        public class WeighbridgeTransactions
        {
            public int id { get; set; }
            public string wbrg_ticket_no { get; set; }
            public string wbrg_ticket_type { get; set; }
            public string wbrg_station { get; set; }
            public string wbrg_roadauthority { get; set; }
            public string wbrg_ticket_weighbridge { get; set; }
            public string wbrg_ticket_source { get; set; }
            public string wbrg_ticket_destination { get; set; }
            public DateTime wbrg_ticket_date { get; set; }
            public DateTime wbrg_ticket_dateout { get; set; }
            public string wbrg_product { get; set; }
            public string wbrg_tx_code { get; set; }
            public string veh_reg { get; set; }
            public string anpr_veh_reg { get; set; }
            public double axle1_weight { get; set; }
            public double axle1_limit { get; set; }
            public DateTime? axle1_time { get; set; }
            public string axle1_group_type1 { get; set; }
            public string axle1_group_type2 { get; set; }
            public double axle2_weight { get; set; }
            public double axle2_limit { get; set; }
            public DateTime? axle2_time { get; set; }
            public string axle2_group_type1 { get; set; }
            public string axle2_group_type2 { get; set; }
            public double axle3_weight { get; set; }
            public double axle3_limit { get; set; }
            public DateTime? axle3_time { get; set; }
            public string axle3_group_type1 { get; set; }
            public string axle3_group_type2 { get; set; }
            public double axle4_weight { get; set; }
            public double axle4_limit { get; set; }
            public DateTime? axle4_time { get; set; }
            public string axle4_group_type1 { get; set; }
            public string axle4_group_type2 { get; set; }
            public double axle5_weight { get; set; }
            public double axle5_limit { get; set; }
            public DateTime? axle5_time { get; set; }
            public string axle5_group_type1 { get; set; }
            public string axle5_group_type2 { get; set; }
            public double axle6_weight { get; set; }
            public double axle6_limit { get; set; }
            public DateTime? axle6_time { get; set; }
            public string axle6_group_type1 { get; set; }
            public string axle6_group_type2 { get; set; }
            public double axle7_weight { get; set; }
            public double axle7_limit { get; set; }
            public DateTime? axle7_time { get; set; }
            public string axle7_group_type1 { get; set; }
            public string axle7_group_type2 { get; set; }
            public double axle8_weight { get; set; }
            public double axle8_limit { get; set; }
            public DateTime? axle8_time { get; set; }
            public string axle8_group_type1 { get; set; }
            public string axle8_group_type2 { get; set; }
            public double wbrg_ticket_grossweight { get; set; }
            public double wbrg_ticket_tareweight { get; set; }
            public double wbrg_ticket_stamp { get; set; }
            public string wbrg_ticket_operator { get; set; }
            public string wbrg_ticket_status { get; set; }
            public string wbrg_ticket_state { get; set; }
            public string wbrg_ticket_printed { get; set; }
            public string wbrg_ticket_direction { get; set; }
            public string wbrg_ticket_directionarea { get; set; }
            public string wbrg_ticket_axel { get; set; }
            public string wbrg_ticket_ref { get; set; }
            public double wbrg_ticket_excessweight { get; set; }
            public string wbrg_ticket_driver { get; set; }
            public string wbrg_ticket_shift { get; set; }
            public string wbrg_ticket_wim { get; set; }
            public string wbrg_ticket_notes { get; set; }
            public string wbgr_ticket_trailer_no { get; set; }
            public string wbrg_ticket_excessload { get; set; }
            public int yardlistid { get; set; }
            public string wbrg_ticket_case_id { get; set; }
            public int wbrg_ticket_reweigh_no { get; set; }
            public int wbrg_ticket_tagged { get; set; }
            public string wbrg_ticket_charge_no { get; set; }
            public double wbrg_ticket_axleload { get; set; }
            public double wbrg_ticket_gvwload { get; set; }
            public string permit_no { get; set; }
            public string wbrg_ticket_make { get; set; }
            public string reliefvehreg { get; set; }
            public int exported { get; set; }
            public string yardList { get; set; }

            
        }
        public class Tags
        {
            public int id { get; set; }
            public string taguid { get; set; }
            public string vehreg { get; set; }
            public string reason { get; set; }
            public DateTime datetime { get; set; }
            public string userid { get; set; }
            public string station { get; set; }
            public string type { get; set; }
            public int status { get; set; }
            public string sms { get; set; }
            public string email { get; set; }
            public string removedby { get; set; }
            public DateTime removeddatetime { get; set; }
            public string removedreason { get; set; }
        }
        public class DataExchange

        {
            public int id { get; set; }
            public int weighbridgetickets { get; set; }
            public string connectionType { get; set; }
            public string server_address { get; set; }
            public int server_port { get; set; }
            public string server_email { get; set; }
            public string server_password { get; set; }
            public string client_address { get; set; }
            public string client_email { get; set; }
            public string client_password { get; set; }
            public int tags { get; set; }
            public DateTime timestamp { get; set; }
        }
        public class TagsExchange
        {
            public int id { get; set; }
            public string tagid { get; set; }
            public string station { get; set; }
            public int status { get; set; }
        }

        
    }
}
