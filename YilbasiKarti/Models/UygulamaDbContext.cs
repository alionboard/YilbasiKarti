using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace YilbasiKarti.Models
{
    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext() : base("conStr")
        {

        }

        public DbSet<Kart> Kartlar { get; set; }
    }
}