using KenloadV2API.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using KenloadV2API.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Runtime.InteropServices;


using System.Net.Http;
using System.Net.Http.Headers;

namespace KenloadV2API.Utils.Datasender
{


    public class senddata
    {
         String url = "http://209.50.58.236:4445/api/";
         String token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjU3MzczZGU0LTcwZTQtNDA4Ni05OTU2LTVhZjM5MzgyMTZkNyIsImVtYWlsIjoiemVwaEBhZG1pbi5jb20iLCJzdWIiOiJ6ZXBoQGFkbWluLmNvbSIsImp0aSI6ImU1MGRlYTRiLWI2ZDItNDI3Yi05NTI5LWYwY2RlNGY1ZDA2YSIsIm5iZiI6MTY0Mjk2NjkxMiwiZXhwIjoxNjQzMDEwMTEyLCJpYXQiOjE2NDI5NjY5MTJ9.eyDN2Mj_vvdaTm6gMvCj-zLVtsmN_MwMrSGfS3dBPcQ";
        private Kenloadv2Data _context;
        //public static void Main(string[] args)
        //{
        //    gettoken();

        //    sendmydata();
        //}
        public void Senddata(Kenloadv2Data context)
        {
            _context = context;
        }
        private void gettoken()
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(url);
                //HTTP GET
                var data = new
                {
                    email = "zeph@admin.com",
                    password = "@Admin123"
                };
                //var responseTask = client.PostAsJsonAsync("WeighbridgeTransactions", Selection);
                var responseTask = client.PostAsJsonAsync("AuthManagement/Login", data);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsHttpResponseMessageAsync();
                    //readTask.Wait();

                    //   var students = readTask.Result;

                    Console.WriteLine(readTask);

                }
            }
            Console.ReadLine();
        }
        public void sendmydata()
        {


            //senddata();




            //IQueryable<WeighbridgeTransactions> query = _context.WeighbridgeTransactions;
            //var Selection = query.Where(b => b.wbrg_ticket_status != "P").OrderByDescending(a => a.id).Skip(0).Take(1).ToList();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                client.BaseAddress = new Uri(url);
                //HTTP GET
                var data = new
                {
                    email = "zeph@admin.com",
                    password = "@Admin123"
                };
                var responseTask = client.PostAsJsonAsync("WeighbridgeTransactions", data);
                //var responseTask = client.GetAsync("WeighbridgeTransactions");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsStreamAsync();
                    //readTask.Wait();

                    //   var students = readTask.Result;

                    Console.WriteLine(readTask);

                }
            }
            Console.ReadLine();


        }

    }




}
