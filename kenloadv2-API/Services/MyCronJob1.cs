using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Devart.Data.MySql;
using Microsoft.Extensions.Logging;

namespace KenloadV2API.Services
{
    public class MyCronJob1 : CronJobService
    {
        private readonly ILogger<MyCronJob1> _logger;

        public MyCronJob1(IScheduleConfig<MyCronJob1> config, ILogger<MyCronJob1> logger)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Backup process starts.");
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} Backup job is working.");
            Console.WriteLine("backup processing...");
            string backupfileName = "Kenloadv2backup-" + DateTime.Now.ToString("dd-MM-yy-HHMMss") + ".sql";
            string folderpath = "C:\\Users\\Masterspace\\Desktop\\";
            string constring = "server=127.0.0.1;uid=root;pwd=root;port=3307;";
            //MemoryStream ms = new MemoryStream();
            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                conn.Open();
                Console.WriteLine("Connected to mysql database");
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                         Console.WriteLine("Opened mysql cmd connection");
                        try
                        {
                           Console.WriteLine("auto backup running...");
                            mb.ExportToFile(folderpath + "\\backups\\" + backupfileName);
                            //Task.Delay(3600*5,cancellationToken);
                            //mb.ExportToMemoryStream(ms);
                            Console.WriteLine("done!");
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                        }
                      conn.Close();
                       Console.WriteLine("Connection closed");
                    }
                }
            }

            // bk.backupName = backupfileName;
            // bk.bkPath = folderpath + "\\backups\\" + backupfileName;
            //  _context.Backups.AddAsync(bk);
            //  _context.SaveChangesAsync();
             Console.WriteLine("backup done!");
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CronJob 1 is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
