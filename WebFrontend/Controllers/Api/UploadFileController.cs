using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebFrontend.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Upload")]
    public class UploadFileController : Controller
    {
        private IHostingEnvironment hostingEnvironment;

        public UploadFileController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        // POST: api/Upload
        [HttpPost]
        //[RequestSizeLimit(5242880)]
        public async Task<IActionResult> PostUploadFile(IFormFile file)
        {
            try
            {
                //foreach (IFormFile source in files)
                //{
                //    string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');

                //    filename = this.EnsureCorrectFilename(filename);

                //    using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(filename)))
                //        await source.CopyToAsync(output);
                //}

                //long size = files.Sum(f => f.Length);

                // full path to file in temp location
                var filePath = Path.GetTempFileName();

                if (file.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                var tempfileName = Path.GetFileName(filePath);
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                
                // process uploaded files
                // Don't rely on or trust the FileName property without validation.
                //return Ok(new { count = files.Count, size, filePath });
                return Json(new { success = true, tempfileName, fileName });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }


        }
        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        private string GetPathAndFilename(string filename)
        {
            string path = this.hostingEnvironment.WebRootPath + "\\uploads\\";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path + filename;
        }
    }
}