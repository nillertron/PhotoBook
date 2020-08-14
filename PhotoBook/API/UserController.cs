using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;
using DataAcces.Specific;
namespace PhotoBook.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IBrugerService brugerService;
        private BrugerRepo brugerrepo;

        public UserController(IBrugerService bs, BrugerRepo repo)
        {
            brugerService = bs;
            brugerrepo = repo;
        }
        // GET: api/User
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/User
        [HttpPost]
        public async Task<PB_Bruger> Post([FromBody] PB_Bruger user)
        {
            var confirmedUser = await brugerrepo.AttemptLogin(user.Email, user.Password);
            return confirmedUser;
        }
        [HttpPost("PostCreateUser")]
        public async Task<IActionResult> PostCreateUser([FromBody] PB_Bruger user)
        {
            System.Web.Http.IHttpActionResult result;
            var succes = await brugerrepo.CreateUser(user);
            if (succes)
            {
                return Ok();

            }
            else
            {

                return BadRequest();

            }
        }

        // PUT: api/User/5
        [HttpGet("{id}")]
        public async Task<PB_Bruger> GetById(int id)
        {
            var bruger = await brugerrepo.Get(id);
            if(bruger != null)
            {
                bruger.Password = "";
            }
            return bruger;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
