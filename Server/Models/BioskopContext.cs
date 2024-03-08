using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Models
{
    public class BioskopContext : DbContext {
        private object modelBuilder;

        public DbSet<Bioskop> Bioskopi { get; set; }
        public DbSet<Sala> Sale { get; set; } 
        public DbSet<Film> Filmovi { get; set; }
        public DbSet<Sediste> Sedista { get; set; }

        public DbSet<FormaBioskopa> FormeBioskopa { get; set; }
        public DbSet<SalaFilm> SalaFilmVeza { get; set; }
        public DbSet<SalaSediste> SalaSedisteVeza { get; set; }

        public BioskopContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bioskop>()
            .HasOne(a => a.formaBioskopa)
            .WithOne(a => a.bioskop)
            .HasForeignKey<FormaBioskopa>(c => c.ID);

            modelBuilder.Entity<SalaFilm>()
            .HasKey(x => new { x.salaID, x.filmID});

            modelBuilder.Entity<SalaSediste>()
            .HasKey(y => new { y.salaID, y.sedisteID});
        }
        
    }

}