using DataAcces;
using DataAcces.Specific;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class TokenService : ITokenService
    {
        private TokenRepo repo;
        private BrugerRepo brugerRepo;
        public TokenService(TokenRepo repo)
        {
            this.repo = repo;
        }
        public async Task CreateToken(PB_Token token)
        {
            await repo.Create(token);
        }
        public async Task<bool> ConsumeToken(string guid)
        {
            var succes = await repo.ConsumeToken(guid);
            return succes;
        }
    }
}
