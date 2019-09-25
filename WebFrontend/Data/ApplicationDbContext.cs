using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebFrontend.Models;

namespace WebFrontend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customizations
            builder.Entity<Participation>()
                .HasOne(p => p.Regalo)
                .WithOne(r => r.Participation)
                .HasForeignKey<Regalo>(r => r.id_regalo)
                .HasPrincipalKey<Participation>(p => p.premioSelFrnt_par);
        }

        public DbSet<WebFrontend.Models.Talleres> Talleres { get; set; }

        public DbSet<WebFrontend.Models.Llantas> Llantas { get; set; }

        public DbSet<WebFrontend.Models.Regalo> Regalo { get; set; }

        public DbSet<WebFrontend.Models.Participation> Participation { get; set; }

        public DbSet<WebFrontend.Models.General> General { get; set; }

        public DbSet<WebFrontend.Models.Regaloslimite> Regaloslimite { get; set; }
    }
}
