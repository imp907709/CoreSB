
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using System;
using Microsoft.EntityFrameworkCore;

namespace CoreSB.Domain.Currency.EF
{
    public class CurrencyContext : DbContext
    {
        public CurrencyContext(DbContextOptions<CurrencyContext> options) : base(options)
        {

        }
        protected CurrencyContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //registration in startup.cs
            //optionsBuilder.UseSqlServer(@"Servedotnet buildr=AAAPC;Database=testdb;User Id=tl;Password=QwErT123;");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CurrencyRatesDAL>().HasOne(p => p.CurrencyFrom)
                .WithMany(p => p.CurRatesFrom).HasForeignKey(p => p.CurrencyFromId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<CurrencyRatesDAL>().HasOne(p => p.CurrencyTo)
                .WithMany(p => p.CurRatesTo).HasForeignKey(p => p.CurrencyToId).OnDelete(DeleteBehavior.Restrict);
            //builder.Entity<Currency>().HasMany(p => p.CurRatesFrom).WithOne(p => p.CurrencyFrom).HasForeignKey(k => k.CurrencyFromId).OnDelete(DeleteBehavior.Cascade);
            //builder.Entity<Currency>().HasMany(p => p.CurRatesTo).WithOne(p => p.CurrencyTo).HasForeignKey(k => k.CurrencyToId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<CurrencyDAL>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<CurrencyDAL>().HasKey(k => k.Id);
            builder.Entity<CurrencyRatesDAL>().Property(k => k.Id).ValueGeneratedOnAdd();
            builder.Entity<CurrencyRatesDAL>().HasKey(k => k.Id);

            //Initial data generation

            // builder.Entity<CurrencyDAL>().HasData(
            //     new CurrencyDAL() { Id = 1, Name = "USD", IsoCode = "USD" },
            //     new CurrencyDAL() { Id = 2, Name = "EUR", IsoCode = "EUR" },
            //     new CurrencyDAL() { Id = 3, Name = "GBP", IsoCode = "GBP" },
            //     new CurrencyDAL() { Id = 4, Name = "RUB", IsoCode = "RUB" },
            //     new CurrencyDAL() { Id = 5, Name = "JPY", IsoCode = "JPY" },
            //     new CurrencyDAL() { Id = 6, Name = "AUD", IsoCode = "AUD" },
            //     new CurrencyDAL() { Id = 7, Name = "CAD", IsoCode = "CAD" },
            //     new CurrencyDAL() { Id = 8, Name = "CHF", IsoCode = "CHF" }
            // );
            //
            // builder.Entity<CurrencyRatesDAL>().HasData(
            //     new CurrencyRatesDAL() { Id = 4, CurrencyFromId = 1, CurrencyToId = 4, Rate = 63.18M, Date = new DateTime(2019, 07, 23) },
            //     new CurrencyRatesDAL() { Id = 5, CurrencyFromId = 2, CurrencyToId = 4, Rate = 70.64M, Date = new DateTime(2019, 07, 23) },
            //     new CurrencyRatesDAL() { Id = 6, CurrencyFromId = 3, CurrencyToId = 4, Rate = 78.67M, Date = new DateTime(2019, 07, 23) },
            //
            //     new CurrencyRatesDAL() { Id = 7, CurrencyFromId = 2, CurrencyToId = 5, Rate = 85.2M, Date = new DateTime(2019, 07, 23) },
            //     new CurrencyRatesDAL() { Id = 8, CurrencyFromId = 3, CurrencyToId = 5, Rate = 95.2M, Date = new DateTime(2019, 07, 23) },
            //
            //     new CurrencyRatesDAL() { Id = 9, CurrencyFromId = 2, CurrencyToId = 6, Rate = 15M, Date = new DateTime(2019, 07, 23) },
            //     new CurrencyRatesDAL() { Id = 10, CurrencyFromId = 6, CurrencyToId = 3, Rate = 0.25M, Date = new DateTime(2019, 07, 23) }
            // );
        }

        public DbSet<CurrencyDAL> Currency { get; set; }
        public DbSet<CurrencyRatesDAL> CurrencyRates { get; set; }

    }

    public class CurrencyContextRead : CurrencyContext
    {
        public CurrencyContextRead(DbContextOptions<CurrencyContextRead> options)
            : base(options) { }
    }
    public class CurrencyContextWrite : CurrencyContext
    {
        public CurrencyContextWrite(DbContextOptions<CurrencyContextWrite> options)
            : base(options) { }
    }
}
