using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class PB_Video:BaseEntity, IMedia
    {
        public string Url { get; set; }
        public string Beskrivelse { get; set; }
        public int PB_FotoalbumId { get; set; }
        public DateTime OprettetDato { get; set; }
    }
}
