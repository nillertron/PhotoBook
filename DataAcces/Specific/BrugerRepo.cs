using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Specific
{
    public class BrugerRepo:Repository<PB_Bruger>
    {
        public BrugerRepo(Context dbContext):base(dbContext)
        {

        }
        public async Task<PB_Bruger> GetBrugerFromSharedId(string id)
        {
            var bruger = dbContext.PB_Bruger.Where(x => x.SharedId == id).Include(x => x.Fotoalbum).ThenInclude(x => x.Fotos).FirstOrDefault();
            if (bruger == null)
                return null;
            foreach(var fa in bruger.Fotoalbum)
            {
                fa.Fotos = fa.Fotos.OrderByDescending(x => x.OprettetDato).ToList();
            }
            return bruger;
        }
        public async Task<PB_Bruger> GetBrugerFromIdInnerJoinFotos(int id)
        {
            var bruger = dbContext.PB_Bruger.Where(x => x.Id == id).Include(x => x.Fotoalbum).ThenInclude(x => x.Fotos).Include(x=>x.Fotoalbum).ThenInclude(x=>x.Videos).FirstOrDefault();
            if (bruger == null)
                return null;
            foreach (var fa in bruger.Fotoalbum)
            {
                fa.Fotos = fa.Fotos.OrderByDescending(x => x.OprettetDato).ToList();
            }
            return bruger;
        }
        public async Task<PB_Bruger> AttemptLogin(string email, string password)
        {
            var user = dbContext.PB_Bruger.Where(x => x.Email.ToLower() == email.ToLower() && password.ToLower() == x.Password.ToLower()).Include(x => x.Fotoalbum).ThenInclude(x => x.Fotos).FirstOrDefault();
            if (user == null)
                throw new Exception("Wrong credentials");
            else
                return user;
        }
        public async Task<bool> CreateUser(PB_Bruger model)
        {
            bool succes = false;
            var user = dbContext.PB_Bruger.Where(x => x.Email.ToLower() == model.Email.ToLower()).FirstOrDefault();
            if(user == null)
            {
                 user = dbContext.PB_Bruger.Where(x => x.SharedId.ToLower() == model.SharedId.ToLower()).FirstOrDefault();

            }
            if(user == null)
            {
                dbContext.PB_Bruger.Add(model);
                await dbContext.SaveChangesAsync();
                succes = true;
            }
            return succes;
        }

    }
}
