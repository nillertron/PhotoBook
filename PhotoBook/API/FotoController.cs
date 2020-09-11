using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using Newtonsoft.Json;
using Model;
using System.Web;
using DataAcces;

namespace PhotoBook.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotoController : ControllerBase
    {
        private IHostingEnvironment hosting;
        private IRepository<PB_Foto> repo;
        public FotoController(IHostingEnvironment hosting, IRepository<PB_Foto> fotoRepo)
        {
            this.hosting = hosting;
            repo = fotoRepo;
        }


        // POST: api/Foto
        [HttpPost("upload")]
        public async Task<string> Upload([FromForm]IFormFile file)
        {
;

            if(file == null || file.Length == 0)
                return "No file";
            string filename = file.FileName;
            string extension = Path.GetExtension(filename);

            string[] allowedExtension = { ".jpg", ".jpeg", ".png", ".bmp" };
            if (!allowedExtension.Contains(extension))
                return "Invalid file type";

            string newFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = hosting.WebRootPath + "/images/";
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            filePath = Path.Combine(filePath, newFileName);

            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(fs);
            }

            return newFileName;


        }
        [HttpPost("UploadToDb")]
        public async Task<string> SaveDbInfo([FromBody] PB_Foto model)
        {
            try
            {
                await repo.Create(model);
                return "Ok";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpPost("Edit")]
        public async Task<string> Edit([FromBody] PB_Foto model)
        {
            try
            {
                await repo.Edit(model);
                return "Ok";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpPost("Delete")]
        public async Task<string> Delete([FromBody] PB_Foto model)
        {
            try
            {
                await repo.Delete(model);
                var fileName = model.Url.Replace("http://photobook.nillertron.com/images/", "");
                var fileLocation = Path.Combine(hosting.WebRootPath, "images", fileName);
                System.IO.File.Delete(fileLocation);
                return "Ok";

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
