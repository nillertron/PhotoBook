using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAcces.Specific;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace PhotoBook.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotoalbumController : ControllerBase
    {
        private FotoalbumRepo repo;
        private BrugerRepo brugerRepo;
        public FotoalbumController(FotoalbumRepo repo,BrugerRepo brugerRepo)
        {
            this.repo = repo;
            this.brugerRepo = brugerRepo;
        }
        // GET: api/Fotoalbum
        [HttpGet("{id}")]
        public async Task<IEnumerable<PB_Fotoalbum>> GetForUser(int id)
        {
            var list = await repo.GetFromBrugerId(id);
            return list;
        }

        // POST: api/Fotoalbum
        [HttpPost("create")]
        public async Task<IActionResult>Create([FromBody] PB_Fotoalbum model)
        {
            try
            {
                await repo.Create(model);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
