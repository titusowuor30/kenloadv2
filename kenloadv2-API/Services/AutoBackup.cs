using Devart.Data.MySql;
using KenloadV2API.Data;
using KenloadV2API.Models;
using System;

namespace KenloadV2API.Services
{
    public class AutoBackup
    {
        private readonly Kenloadv2Data _context;
        public AutoBackup(Kenloadv2Data context)
        {
            _context=context;
        }
        public void AutoDbBackup()
        {
            Backups bk = new Backups();
            Console.WriteLine("Backup process running...");
            //string backupfileName = "Kenloadv2backup-" + DateTime.Now.ToString("dd-MM-yy-HHMMss") + ".sql";
            //string folderpath = "C:\\Users\\Masterspace\\Desktop\\";
            //string constring = Globals.conString;
            //MemoryStream ms = new MemoryStream();
            // using (MySqlConnection conn = new MySqlConnection(constring))
            // {
            //     using (MySqlCommand cmd = new MySqlCommand())
            //     {
            //         using (MySqlBackup mb = new MySqlBackup(cmd))
            //         {
            //             cmd.Connection = conn;
            //             conn.Open();
            //             try
            //             {
            //                 mb.ExportToFile(folderpath + "\\backups\\" + backupfileName);
            //                 //mb.ExportToMemoryStream(ms);
            //             }
            //             catch (Exception ex)
            //             {
            //                 ex.Message.ToString();
            //             }
            //             conn.Close();
            //         }
            //     }
            // }

            // bk.backupName = backupfileName;
            // bk.bkPath = folderpath + "\\backups\\" + backupfileName;
            //  _context.Backups.AddAsync(bk);
            //  _context.SaveChangesAsync();
            Console.WriteLine("Backup complete");
        }
    }
}
