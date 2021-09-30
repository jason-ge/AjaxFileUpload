using AjaxFileUploadWebApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AjaxFileUploadWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;
        private IWebHostEnvironment _hostingEnvironment;
        public FileController(ILogger<FileController> logger, IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
            _logger = logger;
        }

        [HttpGet]
        public string HealthCheck()
        {
            return "Good to go!";
        }

        [HttpPost("Upload")]
        public async Task<AjaxResultcs> Upload(IFormFile file)
        {
            if (file != null)
            {
                // Make sure the uploadFolder grants everyone Modify permission for testing purpose.
                string uploadFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");
                string filePath = Path.Combine(uploadFolder, file.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            return new AjaxResultcs() { IsSuccessful = true, Message = "" };
        }
    }
}
