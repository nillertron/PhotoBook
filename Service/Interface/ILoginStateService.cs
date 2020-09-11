using Model;
using System.Threading.Tasks;

namespace Service
{
    public interface ILoginStateService
    {
        PB_Bruger user { get; }

        Task EditUser(PB_Bruger model);
        Task GetUserFromId(int id);
        Task Login(string email, string password);
        Task Logout();
    }
}