using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Specific
{
    public class TokenRepo:Repository<PB_Token>
    {
        public TokenRepo(Context dbContext) : base(dbContext)
        {

        }

        public async Task<bool> ConsumeToken(string guid)
        {
            var token = dbContext.PB_Token.Where(x => x.Token == guid && x.ExpirationDate > DateTime.Now).FirstOrDefault();
            if (token == null)
                return false;
            return true;

        }

    }
}
