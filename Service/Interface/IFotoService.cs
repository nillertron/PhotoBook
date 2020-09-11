using Model;
using System.Threading.Tasks;

namespace Service
{
    public interface IFotoService
    {
        Task DeleteFotoAPIAsync(PB_Foto foto);
        Task Get();
        Task UploadPhoto(string location, string beskrivelse, int albumId);
        Task UpdateFotoAPIAsync(PB_Foto foto);
    }
}