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
            var bruger = await brugerrepo.GetBrugerFromIdInnerJoinFotos(id);
            if(bruger != null)
            {
                bruger.Password = "";
            }
            return bruger;
        }
        [HttpPost("Edit")]
        public async Task<string> Edit([FromBody] PB_Bruger user)
        {
            try
            {
                var dbUser = await brugerrepo.Get(user.Id);
                if (dbUser == null)
                    throw new Exception("User not found");
                dbUser.Navn = user.Navn;
                dbUser.EfterNavn = user.EfterNavn;
                dbUser.Password = user.Password;
                await brugerrepo.Edit(dbUser);
                return "Ok";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
