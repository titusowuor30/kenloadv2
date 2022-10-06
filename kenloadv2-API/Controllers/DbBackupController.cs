using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KenloadV2API.Data;
using KenloadV2API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using MySqlConnector;
using System.IO;
using System.Text;
using System.Linq;
using Aspose.Zip.Saving;
using Aspose.Zip;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace KenloadV2API.Controllers
{
    [EnableCors("ServerPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class DbBackupController : ControllerBase
    {
        private readonly Kenloadv2Data _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DbBackupController(Kenloadv2Data context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        //backup list 
        [HttpGet("backupfiles")]
        // GET: api/Shifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Backups>>> GetFiles()
        {
            return await _context.Backups.OrderByDescending(x => x.id).ToListAsync();
        }

        //download
        [HttpGet("downloadBackup")]
        public IActionResult GetZipFile(string filename,string filePath)
        {
            if (filename.Contains(".sql") || filePath.Contains(".sql"))
            {
                filename.Replace(@".sql", @"");
                filename.Trim();
                filePath = filePath.Replace(@".sql", @".zip").Trim();
            }
                const string contentType = "application/zip";
                HttpContext.Response.ContentType = contentType;
                var result = new FileContentResult(System.IO.File.ReadAllBytes(@filePath), contentType)
                {
                    FileDownloadName = $"{filename}.zip"
                };
                return result;
        }


        [HttpPost("CreateBackup")]
        public async Task<ActionResult<Backups>> BackupDatabase(Backups bk,string folderpath, string backupName)
        {
            string constring = Globals.conString;
            string clenaedBackupName=string.Empty;
            Regex rgx = new Regex("[^A-Za-z0-9]");
            for (int i = 0; i < backupName.Length; i++)
            {
                clenaedBackupName += (rgx.IsMatch(backupName[i].ToString()) ? "_": backupName[i].ToString());
            }
            clenaedBackupName = (clenaedBackupName+DateTime.Now.ToString("yyyy-MM-dd_hh-MM-ss-ms")+ ".sql").Trim();
            folderpath = folderpath.Trim();
            //MemoryStream ms = new MemoryStream();
            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        try
                        {
                         mb.ExportToFile(folderpath+ "\\backups\\"+ clenaedBackupName);
                           //mb.ExportToMemoryStream(ms);
                        }
                        catch (Exception ex)
                        {
                            return BadRequest(ex.Message);  
                        }
                        conn.Close();
                    }
                }
            }
            string zipName =clenaedBackupName.Replace(@".sql",@".zip");
            string backupPath = folderpath + "\\backups\\"+zipName;
            using var archive =ZipFile.Open(backupPath, ZipArchiveMode.Create);
            var entry = archive.CreateEntryFromFile(folderpath+"\\backups\\"+clenaedBackupName,Path.GetFileName(clenaedBackupName),CompressionLevel.Optimal);
            bk.backupName = zipName;
            bk.bkPath = backupPath;
            await _context.Backups.AddAsync(bk);
            await _context.SaveChangesAsync();
            return Ok("Backup created! File:"+ backupPath);
        }
        [HttpPost("restoreBackup")]
        public string restoreBackup(string folderpath, string backupName)
        {
            string constring = Globals.conString;
            if (backupName.Contains(".zip")) {
                backupName = backupName.Replace(@".zip", @".sql");
            }
            else
            {
                backupName = backupName.Trim();
            }
            var file = folderpath + "\\backups\\" + backupName;
            var fileExist = new FileInfo(file);
            if (fileExist.Exists)
            {
                using (MySqlConnection conn = new MySqlConnection(constring))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            try
                            {
                                mb.ImportFromFile(file);
                            }
                            catch (Exception ex)
                            {
                                ex.Message.ToString();
                            }
                            conn.Close();
                            return "System restore success!Restore file => " + file;
                        }
                    }
                }
            }
            else
            {
                return "System restore Failed!Restore file" + file +"does not exist!";
            }
        }
    }
}