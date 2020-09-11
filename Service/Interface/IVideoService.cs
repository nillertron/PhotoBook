using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public interface IVideoService
    {
        Task DeleteVideo(PB_Video video);
        Task EditVideo(PB_Video video);
        Task<List<PB_Video>> GetUserVideosFromFotoAlbumId(int id);
        Task<string> SaveVideoToDb(PB_Video video);
        Task<string> UploadVideo(string location, string beskrivelse, int albumdId);
    }
}