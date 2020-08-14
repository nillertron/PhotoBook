using Model;
using System.Threading.Tasks;

namespace Service
{
    public interface ILoginStateService
    {
        PB_Bruger user { get; }

        Task GetUserFromId(int id);
        Task Login(string email, string password);
        Task Logout();
    }
}