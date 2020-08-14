using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PB_Foto:BaseEntity
    {
        public string Url { get; set; }
        public string Beskrivelse { get; set; }
        public DateTime OprettetDato { get; set; }
        public int PB_FotoalbumId { get; set; }
    }
}
