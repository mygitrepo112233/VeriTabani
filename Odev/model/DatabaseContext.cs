using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Odev.Controllers;
using static Odev.model.Models;

namespace Odev.model {
    public class DatabaseContext : DbContext {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        //Veritabanı tablolarınızı temsil eden DbSet'ler
        public DbSet<Hasta> hastalar { get; set; }
        public DbSet<Doktor> doktorlar { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Doktor>()
                .ToTable("doktorlar")
                .HasBaseType<Personel>();
        }
        public DbSet<Personel> personel { get; set; }

        public DbSet<Ameliyat> ameliyatlar { get; set; }

        public DbSet<Hastalik> hastaliklar { get; set; }
        public DbSet<Ilac> ilaclar { get; set; }
        public DbSet<Maliye> maliye { get; set; }

        public DbSet<Oda> odalar { get; set; }

        public DbSet<Poliklinik> poliklinikler { get; set; }
        public DbSet<Randevu> randevular { get; set; }

        public DbSet<Recete> receteler { get; set; }

        public DbSet<Tahlil> tahliller { get; set; }

        public DbSet<Yatis> yatislar { get; set; }
    }
}
