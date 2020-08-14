using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PB_Bruger:BaseEntity
    {
        public string SharedId { get; set; }
        public string Navn { get; set; }
        public string EfterNavn { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<PB_Fotoalbum> Fotoalbum { get; set; } = new List<PB_Fotoalbum>();
    }
}
