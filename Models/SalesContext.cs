using Microsoft.EntityFrameworkCore;

namespace aspnetapp.Models
{
    public class SalesContext : DbContext
    {
        public SalesContext(DbContextOptions<SalesContext> options)
            : base(options)
        {
        }
        public DbSet<Sales> SalesData { get; set; }
    }
}