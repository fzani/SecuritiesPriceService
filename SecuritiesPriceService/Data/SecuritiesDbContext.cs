using BNP.SecuritiesPriceService.Models;
using System.Data.Entity;

namespace BNP.SecuritiesPriceService.Data
{
    public class SecuritiesDbContext : DbContext
    {
        // Parameterless constructor which uses the connection string from App.config or Web.config
        public SecuritiesDbContext() : base("name=SecuritiesDbContext")
        {
        }

        // Constructor that accepts a connection string directly
        public SecuritiesDbContext(string connectionString) : base(connectionString)
        {
        }

        // DbSet properties
        public DbSet<Security> Securities { get; set; }
    }
}
