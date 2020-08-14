using System.Threading.Tasks;

namespace Service
{
    public interface IFotoService
    {
        Task Get();
        Task UploadPhoto(string location, string beskrivelse, int albumId);
    }
}