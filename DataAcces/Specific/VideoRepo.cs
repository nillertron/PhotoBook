using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Specific
{
   public class VideoRepo:Repository<PB_Video>
    {
        public VideoRepo(Context dbContext) : base(dbContext)
        {

        }
        public async Task<List<PB_Video>> GetUserVideoFromAlbumId(int id)
        {
            var list = dbContext.PB_Video.Where(x => x.PB_FotoalbumId == id).ToList();
            return list;
        }
    }
}
