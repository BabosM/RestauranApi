using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi.Controllers
{
    [Route("file")]
   //Authorize]
    public class FileController : ControllerBase
    {
     
        [HttpGet]
        [ResponseCache(Duration = 120, VaryByQueryKeys = new[] { "fileName" })]
        public ActionResult GetFile([FromQuery] string fileName)
        {
            var rootPath = Directory.GetCurrentDirectory();
            var filePath = $"{rootPath}/PrivateFiles/{fileName}";

            var fileExisits =  System.IO.File.Exists(filePath);

            if (!fileExisits)
            { 
                return NotFound();
            }
            // my solution zwroci .txt przez co otrzymujemy error. Ponizsze rozwiazanie zwroci txt/plane
            //var fileExtension = Path.GetExtension(filePath);

            var contentProvider = new FileExtensionContentTypeProvider();
            contentProvider.TryGetContentType(fileName, out string fileExtension);
            var fileContents =  System.IO.File.ReadAllBytes(filePath);
            return File(fileContents, fileExtension, fileName);
        }
        [HttpPost]
        public ActionResult UploadFile([FromQuery]IFormFile file)
        {
            if(file != null || file.Length > 0)
            {
                var rootPath = Directory.GetCurrentDirectory();
                var name = file.FileName;
                var filePath = $"{rootPath}/PrivateFiles/{name}";
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Ok();
            }
            return BadRequest();
        }
    }
}
