using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataAcces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhotoBook.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private IHostingEnvironment hosting;
        private IRepository<PB_Video> repo;
        public VideoController(IHostingEnvironment hosting, IRepository<PB_Video> videoRepo)
        {
            this.hosting = hosting;
            repo = videoRepo;
        }

        [HttpPost("upload")]
        public async Task<string> Upload([FromForm] IFormFile file)
        {
            if (file == null)
                return "File null";
            var filNavn = file.FileName;
            var extension = Path.GetExtension(filNavn);
            filNavn = Guid.NewGuid().ToString();
            filNavn += extension;

            var rootPath = Path.Combine(hosting.WebRootPath, "video");
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            var savePath = Path.Combine(rootPath, filNavn);

            using (var fs = new FileStream(savePath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
                return filNavn;
            }
            

        }
        [HttpPost("savedb")]
        public async Task<string> SaveToDb([FromBody] PB_Video video)
        {
            try
            {
                await repo.Create(video);
                return "Ok";
            }
            catch(Exception e)
            {
                return "not ok";
            }
        }
        [HttpPost("Delete")]
        public async Task<string> Delete([FromBody] PB_Video video)
        {
            try
            {
                var fileName = video.Url.Replace("http://photobook.nillertron.com/video/", "");
                await repo.Delete(video);
                var fileLocation = Path.Combine(hosting.WebRootPath, "video", fileName);
                System.IO.File.Delete(fileLocation);
                return "Ok";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        [HttpPost("Edit")]
        public async Task<string> Edit([FromBody] PB_Video video)
        {
            try
            {
                await repo.Edit(video);
                return "Ok";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}
