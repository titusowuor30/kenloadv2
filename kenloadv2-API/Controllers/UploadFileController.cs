using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using System.IO.Compression;
using System.Drawing.Drawing2D;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KenloadV2API.Controllers
{
    [EnableCors("ServerPolicy")]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        // GET: api/<UploadFileController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2", "this is just get value" };
        }

        // GET api/<UploadFileController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UploadFileController>
        [HttpPost("uploadSingle")]
        /* public void Post([FromBody] string value)
         {
         }*/
        public async Task<IActionResult> OnPostUploadAsync(IFormFile files,string folderPath, string prefix="")
        {
            //check if images directory exists
            //checkImagesDirectory();
            //var pathsToFile = "";
            //line.Replace(@",", @";");
            folderPath = HttpUtility.UrlDecode(folderPath);
            //files.Name = filename;

                try
                {
                    if (Directory.Exists(folderPath))
                    {
                    if (files.Length > 0)
                    {
                        string fName = prefix+files.FileName;
                        string path = Path.Combine(@folderPath +'/'+fName+".jpg");
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await files.CopyToAsync(stream);
                        }
                    }
                    //The code will execute if the folder exists
                }
                    //The below code will create a folder if the folder is not exists in C#.Net.            
                    DirectoryInfo folder = Directory.CreateDirectory(@folderPath);
                if (files.Length > 0)
                {
                    string fName = files.FileName;
                    string path = Path.Combine(@folderPath + '/' + fName + ".jpg");
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await files.CopyToAsync(stream);
                    }
                }
            }
                catch (Exception e)
                {

                }
                finally
                {

                }
            


            

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { files.FileName});
        }

        [HttpPost("uploadMultiple")]
        /* public void Post([FromBody] string value)
         {
         }*/
        public async Task<IActionResult> OnPostUploadAsyncMutiple(List<IFormFile> files, string folderPath, string prefix = "")
        {
            //checkImagesDirectory();//check if images directpry exists
            long size = files.Sum(f => f.Length);
            var filePath = Path.GetTempFileName();
            int i = 0;
            try
            {
                
                foreach (var formFile in files)
                {
                    i++;
                    if (formFile.Length > 0)
                    {
                        var ext = Path.GetExtension(formFile.FileName);
                        string fName = prefix + i +ext;
                        string targetFolder = Path.Combine(folderPath);
                        string path = Path.Combine(folderPath+"\\" + fName);
                        if (!Directory.Exists(@targetFolder))
                        {
                            DirectoryInfo folder = Directory.CreateDirectory(@targetFolder);
                        }
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                      
                    }
                }
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size, filePath });
        }

        // PUT api/<UploadFileController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpGet("ConvertBase64")]
        public async Task<string> convertbase64Async(string url)
        {
            using (var client = new HttpClient())
            {
                var bytes = await client.GetByteArrayAsync(url);
                var compressedBytes = Compress(bytes);
                //data:image/jpeg;base64,
                var convertedImagePng = ConvertImageBytes(compressedBytes, ImageFormat.Png);
                return "data:image/png;base64," + Convert.ToBase64String(convertedImagePng);
            }

        }
        [HttpGet("CaptureImage")]

        public string captureStillImage(string folderpath,string imageName, string url, string username, string password)
        {
            //imageName = imageName + ".jpg";
            imageName = imageName + "";

            String base64String = "";
            WebClient client = new WebClient();
            client.Credentials = new NetworkCredential(username, password);
            Stream stream = client.OpenRead(url);
            Bitmap bitmap; bitmap = new Bitmap(stream);
          
            try
            {
                stream = client.OpenRead(url);
                bitmap = new Bitmap(stream);
                if (bitmap != null)
                {
                    bitmap.Save(folderpath + imageName, ImageFormat.Jpeg);
                }

                
                    stream.Flush();
                stream.Close();
                //convert image to base64

                using (Image image = Image.FromFile(folderpath + imageName))
                {
                    //bitmap.Save(folderpath + imageName, ImageFormat.Jpeg);
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();
                        base64String = Convert.ToBase64String(imageBytes);
                        return base64String;
                    }
                }

            }
            catch (Exception)
            {

                stream.Flush();
                stream.Close();
            }



            client.Dispose();

            return base64String;

        }
        // DELETE api/<UploadFileController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
        [HttpGet("GetANPR")]
        public String getAnpr(string folderpath, string imageName, string url, string username, string password)
        {

            String result = "";


            WebClient client = new WebClient();
            client.Credentials = new NetworkCredential(username, password);
            Bitmap bitmap = null;
            Stream stream = null;
            try
            {
                stream = client.OpenRead(url);
                bitmap = new Bitmap(stream);
                if (bitmap != null)
                {
                    bitmap.Save(folderpath + imageName, ImageFormat.Jpeg);
                }

                stream.Flush();
                stream.Close();



                Process proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "C:\\Kenload_Weighing_System\\openalpr_32\\alpr.exe",
                        Arguments = " -c eu -j " + folderpath + imageName,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();

                proc.WaitForExit();


                while (!proc.StandardOutput.EndOfStream)
                {
                    result += proc.StandardOutput.ReadLine();
                    // do something with line

                }
                if (result.Contains("License is expired"))
                {

                    WebClient client2 = new WebClient();
                    string text = client2.DownloadString("https://micna.co.ke/license/license.txt");
                    
                    String FileName = "C:\\Kenload_Weighing_System\\openalpr_32\\license.conf";
                    System.IO.File.WriteAllText(FileName, text);
                  


                    proc = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "C:\\Kenload_Weighing_System\\openalpr_32\\alpr.exe",
                            Arguments = " -c eu -j " + folderpath + imageName,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };
                    proc.Start();

                    proc.WaitForExit();
                    while (!proc.StandardOutput.EndOfStream)
                    {
                        result += proc.StandardOutput.ReadLine();
                        // do something with line

                    }

                    FileName = "H:\\Kenload_Weighing_System\\openalpr_32\\license.conf";
                    System.IO.File.WriteAllText(FileName, text);

                }
                return result;



            }
            catch (Exception)
            {
                return result;
            }
            return result;


        }
        public static byte[] Compress(byte[] data)
        {
         /*   MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(output, CompressionLevel.Optimal))
            {
                dstream.Write(data, 0, data.Length);
            }
            return output.ToArray()*/;

            using var memoryStream = new MemoryStream(data);
            using var originalImage = new Bitmap(memoryStream);
            var resized = new Bitmap(545, 300);
            using var graphics = Graphics.FromImage(resized);
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.DrawImage(originalImage, 0, 0, 645, 300);
            using var stream = new MemoryStream();
            resized.Save(stream, ImageFormat.Png);
            return stream.ToArray();

        }

        public static byte[] Decompress(byte[] data)
        {
            MemoryStream input = new MemoryStream(data);
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
            {
                dstream.CopyTo(output);
            }
            return output.ToArray();
        }
        public static byte[] ConvertImageBytes(byte[] imageBytes, ImageFormat imageFormat)
        {
            byte[] byteArray = new byte[0];
            FileStream stream = new FileStream("empty." + imageFormat, FileMode.Create);
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                stream.Write(byteArray, 0, byteArray.Length);
                byte[] buffer = new byte[16 * 1024];
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                byteArray = ms.ToArray();
                stream.Close();
                ms.Close();
            }
            return byteArray;
        }

    }
}
