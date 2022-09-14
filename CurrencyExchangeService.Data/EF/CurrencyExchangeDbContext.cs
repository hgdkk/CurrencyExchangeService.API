using CurrencyExchangeService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeService.Data.EF
{
    public class CurrencyExchangeDbContext : DbContext
    {
        public CurrencyExchangeDbContext(DbContextOptions<CurrencyExchangeDbContext> options) : base(options)
        {

        }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Log> Logs { get; set; }
    }
}
