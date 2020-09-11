using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public interface IMedia
    {
         string Url { get; set; }
         string Beskrivelse { get; set; }
         DateTime OprettetDato { get; set; }
         int PB_FotoalbumId { get; set; }
    }
}
