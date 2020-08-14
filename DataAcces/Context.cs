using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace DataAcces
{
    public class Context:DbContext
    {
        public Context(DbContextOptions options):base(options)
        {

        }
        public DbSet<PB_Bruger> PB_Bruger { get; set; }
        public DbSet<PB_Foto> PB_Foto { get; set; }
        public DbSet<PB_Fotoalbum> PB_FotoAlbum { get; set; }
    }
}
