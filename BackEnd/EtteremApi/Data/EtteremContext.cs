using EtteremApi.Adatmodel;
using Microsoft.EntityFrameworkCore;

namespace EtteremApi.Data
{
    public class EtteremContext : DbContext
    {
        public EtteremContext(DbContextOptions<EtteremContext> options) : base(options)
        {
        }

        public DbSet<Felhasznalo> Felhasznalok => Set<Felhasznalo>();
        public DbSet<Alkalmazott> Alkalmazottak => Set<Alkalmazott>();
        public DbSet<Asztal> Asztalok => Set<Asztal>();
        public DbSet<Foglalas> Foglalasok => Set<Foglalas>();
        public DbSet<FoglalasAsztal> FoglalasAsztalok => Set<FoglalasAsztal>();
        public DbSet<KiadasKategoria> KiadasKategoriak => Set<KiadasKategoria>();
        public DbSet<Kiadas> Kiadasok => Set<Kiadas>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Felhasznalo>(e =>
            {
                e.ToTable("felhasznalok");
                e.Property(x => x.Nev).HasMaxLength(100).IsRequired();
                e.Property(x => x.Email).HasMaxLength(150).IsRequired();
                e.HasIndex(x => x.Email).IsUnique();
                e.Property(x => x.JelszoHash).HasMaxLength(255).IsRequired();
                e.Property(x => x.Telefon).HasMaxLength(30);
                e.Property(x => x.Szerepkor).HasConversion<string>().HasMaxLength(20);
            });

            mb.Entity<Alkalmazott>(e =>
            {
                e.ToTable("alkalmazottak");
                e.Property(x => x.Munkakor).HasMaxLength(80).IsRequired();
                e.Property(x => x.Ber).HasPrecision(10, 2);
                e.HasOne(x => x.Felhasznalo)
                 .WithOne(f => f.Alkalmazott)
                 .HasForeignKey<Alkalmazott>(x => x.FelhasznaloId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            mb.Entity<Asztal>(e =>
            {
                e.ToTable("asztalok");
                e.HasIndex(x => x.Szam).IsUnique();
            });

            mb.Entity<Foglalas>(e =>
            {
                e.ToTable("foglalasok");
                e.Property(x => x.Megjegyzes).HasMaxLength(500);
                e.Property(x => x.Statusz).HasConversion<string>().HasMaxLength(20);
                e.HasOne(x => x.Felhasznalo)
                 .WithMany(f => f.Foglalasok)
                 .HasForeignKey(x => x.FelhasznaloId)
                 .OnDelete(DeleteBehavior.Restrict);
                e.HasOne(x => x.KezeloAlkalmazott)
                 .WithMany(a => a.KezeltFoglalasok)
                 .HasForeignKey(x => x.KezeloAlkalmazottId)
                 .OnDelete(DeleteBehavior.SetNull);
            });

            mb.Entity<FoglalasAsztal>(e =>
            {
                e.ToTable("foglalas_asztalok");
                e.HasKey(x => new { x.FoglalasId, x.AsztalId });
                e.HasOne(x => x.Foglalas)
                 .WithMany(f => f.Asztalok)
                 .HasForeignKey(x => x.FoglalasId)
                 .OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Asztal)
                 .WithMany(a => a.Foglalasok)
                 .HasForeignKey(x => x.AsztalId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            mb.Entity<KiadasKategoria>(e =>
            {
                e.ToTable("kiadas_kategoriak");
                e.Property(x => x.Nev).HasMaxLength(80).IsRequired();
                e.HasIndex(x => x.Nev).IsUnique();
            });

            mb.Entity<Kiadas>(e =>
            {
                e.ToTable("kiadasok");
                e.Property(x => x.Osszeg).HasPrecision(12, 2);
                e.Property(x => x.Leiras).HasMaxLength(300);
                e.HasOne(x => x.Kategoria)
                 .WithMany(k => k.Kiadasok)
                 .HasForeignKey(x => x.KategoriaId)
                 .OnDelete(DeleteBehavior.Restrict);
                e.HasOne(x => x.RogzitetteAlkalmazott)
                 .WithMany(a => a.RogzitettKiadasok)
                 .HasForeignKey(x => x.RogzitetteAlkalmazottId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            mb.Entity<KiadasKategoria>().HasData(
                new KiadasKategoria { Id = 1, Nev = "Alapanyag" },
                new KiadasKategoria { Id = 2, Nev = "Rezsi" },
                new KiadasKategoria { Id = 3, Nev = "Bér" },
                new KiadasKategoria { Id = 4, Nev = "Karbantartás" },
                new KiadasKategoria { Id = 5, Nev = "Egyéb" });

            mb.Entity<Asztal>().HasData(
                new Asztal { Id = 1, Szam = 1, Ferohely = 2 },
                new Asztal { Id = 2, Szam = 2, Ferohely = 2 },
                new Asztal { Id = 3, Szam = 3, Ferohely = 4 },
                new Asztal { Id = 4, Szam = 4, Ferohely = 4 },
                new Asztal { Id = 5, Szam = 5, Ferohely = 6 });
        }
    }
}