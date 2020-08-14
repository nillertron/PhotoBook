using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PB_Fotoalbum:BaseEntity
    {
        public string Navn { get; set; } = string.Empty;
        public string Beskrivelse { get; set; } = string.Empty;
        public DateTime OprettetDato { get; set; }
        public List<PB_Foto> Fotos { get; set; } = new List<PB_Foto>();
        public int PB_BrugerId { get; set; }
    }
}
