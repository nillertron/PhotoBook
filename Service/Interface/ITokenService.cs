using Model;
using System.Threading.Tasks;

namespace Service
{
    public interface ITokenService
    {
        Task<bool> ConsumeToken(string guid);
        Task CreateToken(PB_Token token);
    }
}