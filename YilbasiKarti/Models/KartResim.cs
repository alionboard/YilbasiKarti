using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YilbasiKarti.Models
{
    public class KartResim
    {
        public Kart Kart { get; set; }
        public IEnumerable<string> Resim { get; set; }
    }
}