using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

using KenloadV2API.Models;
using KenloadV2API.Data;
using System.Net.Http.Headers;
using KenloadV2API.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KenloadV2API
{
    public class Program
    {
   
        public static void Main(string[] args)
        {
            
            CreateHostBuilder(args).Build().Run();
           
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseUrls("https://localhost:44365");
                });//.ConfigureServices(services=>services.AddHostedService<AutoBackup>());   
    }
}
