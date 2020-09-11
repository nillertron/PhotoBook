using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PB_Foto:BaseEntity, IMedia
    {
        public string Url { get; set; } = string.Empty;
        public string Beskrivelse { get; set; } = string.Empty;
        public DateTime OprettetDato { get; set; }
        public int PB_FotoalbumId { get; set; }
    }
}
