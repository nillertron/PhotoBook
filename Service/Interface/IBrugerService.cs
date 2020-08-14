using Model;
using System.Threading.Tasks;

namespace Service
{
    public interface IBrugerService
    {
        Task CrateUserRestApi(PB_Bruger model);
        Task<PB_Bruger> FindUserFromSharedId(string id);
    }
}