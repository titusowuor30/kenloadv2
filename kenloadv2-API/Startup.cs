using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using KenloadV2API.Configuration;
using KenloadV2API.Data;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.Net.Http.Headers;

//mpesa integration
using MpesaLib;
using Mpesa.SDK.AspNetCore;
using KenloadV2API.Datatransfer;
using KenloadV2API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Hangfire;
using Hangfire.MySql;

namespace KenloadV2API
{
    public class Startup
    {

        private readonly Kenloadv2Data _context;
        public AutoBackup autobk;
        public Startup(IConfiguration configuration)
        {
            _config = configuration;

            //  _recurringJobManager = recurringJobManager;
        }

        public IConfiguration _config { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var emailconfig = _config.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            services.AddSingleton(emailconfig);
            services.AddControllers();
            services.AddScoped<IEmailSender, EmailSender>();
            //add mpesa baseadress
            services.AddMpesaSdk(_config);
            //services.AddHttpClient<IMpesaClient, MpesaClient>(options => options.BaseAddress = RequestEndPoint.SandboxBaseAddress);
            services.Configure<JwtConfig>(_config.GetSection("JwtConfig"));
            //services.AddScoped<IUserManager, UserManager>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(jwt =>
           {
               var key = Encoding.ASCII.GetBytes(_config["JwtConfig:Secret"]);

               jwt.SaveToken = true;
               jwt.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateLifetime = true,
                   RequireExpirationTime = false
               };
           });

            services.AddScoped<IDatatransfer, Datatransferclass>();
            //services.AddScoped<IMyScopedService, MyScopedService>();

            //services.AddCronJob<MyCronJob1>(c =>
            //{
            //    c.TimeZoneInfo = TimeZoneInfo.Local;
            //    c.CronExpression = @"*/5 * * * *";
            //});
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KenloadV2API", Version = "v1" });
                c.AddSecurityDefinition(
            "token",
            new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer",
                In = ParameterLocation.Header,
                Name = HeaderNames.Authorization
            }
        );
                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "token"
                        },
                    },
                    Array.Empty<string>()
                }
                    }
                );
            });

            string conStringDb = "server=127.0.0.1;database=kenloadv2;uid=kenload;password='Y0Bwd2VpZ2hAd2ViMjAxOQ';port=3309;Allow User Variables=true;";
            conStringDb = "server=127.0.0.1;database=kenloadv2;uid=root;password='Y0Bwd2VpZ2hAd2ViMjAxOQ==';port=3309;Allow User Variables=true;";

            conStringDb = "server=127.0.0.1;database=kenloadv2;uid=root;password='root';port=3307;Allow User Variables=true;";
            //string conStringDb = "server=192.168.9.22;database=kenloadv2;uid=kenload;password='Y0Bwd2VpZ2hAd2ViMjAxOQ';port=3309;Allow User Variables=true;";

            //string conStringDb = "server=192.168.4.22;database=kenloadv2;uid=kenload;password='Y0Bwd2VpZ2hAd2ViMjAxOQ';port=3309;Allow User Variables=true;"

            // services.AddDbContext<Kenloadv2Data>(opt => opt.UseMySql(connectionString: @"server=127.0.0.1;database=Kenloadv2;uid=root;password='root';port=3309;", new MySqlServerVersion(new Version(8, 0, 21))));
            Globals.conString = conStringDb;
            // services.AddDbContext<Kenloadv2Data>(opt => opt.UseMySql(connectionString: @"server=127.0.0.1;database=Kenloadv2;uid=root;password='Y0Bwd2VpZ2hAd2ViMjAxOQ==';port=3309;", new MySqlServerVersion(new Version(8, 0, 21))));
            //services.AddDbContext<Kenloadv2Data>(opt => opt.UseMySql(connectionString: conStringDb, new MySqlServerVersion(new Version(8, 0, 21))));
            services.AddDbContext<Kenloadv2Data>(opt => opt.UseMySql(connectionString: conStringDb, new MySqlServerVersion(new Version(8, 0, 21))));
            services.AddDirectoryBrowser();
            //services.AddHangfire(configuration => configuration
            // .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            // .UseSimpleAssemblyNameTypeSerializer()
            // .UseRecommendedSerializerSettings()
            // .UseStorage(
            //     new MySqlStorage(
            //         Globals.conString,
            //         new MySqlStorageOptions
            //         {
            //             QueuePollInterval = TimeSpan.FromSeconds(10),
            //             JobExpirationCheckInterval = TimeSpan.FromHours(1),
            //             CountersAggregateInterval = TimeSpan.FromMinutes(5),
            //             PrepareSchemaIfNecessary = true,
            //             DashboardJobListLimit = 25000,
            //             TransactionTimeout = TimeSpan.FromMinutes(1),
            //             TablesPrefix = "Hangfire",
            //         }
            //     )
            // ));
            //services.AddHangfireServer();
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                 .AddEntityFrameworkStores<Kenloadv2Data>();
            services.AddCors(options =>
            {
                options.AddPolicy(name: "ServerPolicy",
                    builder =>
                    {
                        /*builder.WithOrigins("http://localhost:8080")
                                         .AllowAnyHeader()
                                         .AllowAnyMethod();*/
                        builder.WithOrigins("")
                                         .AllowAnyHeader()
                                         .AllowAnyMethod()
                                         .SetIsOriginAllowed(origin => true) // allow any origin
                                         .AllowCredentials(); // allow credential

                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //app.UseHangfireDashboard("/Hangfire");
                //  _recurringJobManager.AddOrUpdate("jobId", () => _datatransfer.ReccuringJob(), Cron.Minutely); 

                // DataBtransferclass transferClass = new Datatransferclass(_context);
                //BackgroundJob.Schedule(()=>Console.WriteLine("backup running..."),TimeSpan.FromMinutes(1));
                //var jobId=BackgroundJob.Enqueue(() => autobk.AutoDbBackup());
                //BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine("Auto backup running in the background..."));
                //RecurringJob.AddOrUpdate("powerfuljob", () =>autobk.AutoDbBackup(), "* * * * *");
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KenloadV2API v1"));
                //transferClass.DataExchange();

            }
            else
            {
                app.UseHsts();
            }
            // var DB = app.ApplicationServices.GetRequiredService<Kenloadv2Data>();
            // DB.Database.EnsureCreated();

            app.UseHttpsRedirection();
            //app.UseHangfireDashboard("/BackupProcesses");
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Could not find anything");
            });
        }



    }
}
