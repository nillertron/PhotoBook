using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Specific
{
    public class FotoalbumRepo:Repository<PB_Fotoalbum>
    {
        public FotoalbumRepo(Context dbContext) : base(dbContext)
        {
            
        }
        public async Task<List<PB_Fotoalbum>> GetFromBrugerId(int id)
        {
            return dbContext.PB_FotoAlbum.Where(x => x.PB_BrugerId == id).ToList();
        }
    }
}
