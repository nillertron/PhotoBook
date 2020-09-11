using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public interface IFotoalbumService
    {
        Task CreateApi(PB_Fotoalbum model);
        Task DeleteApi(PB_Fotoalbum model);
        Task<List<PB_Fotoalbum>> GetAllForUserApi(int userId);
        Task<PB_Fotoalbum> GetApi(int photoAlbumId);
        Task<List<PB_Foto>> GetFotoAlbum(int id);
    }
}