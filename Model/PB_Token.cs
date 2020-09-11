using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PB_Token:BaseEntity
    {       
        public string Token { get; set; }
        public PB_Bruger Bruger { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
