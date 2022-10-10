using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace imparTest.Models
{
    public class imparTestContext : DbContext
    {
        public imparTestContext(DbContextOptions<imparTestContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Photo> Photo { get; set; } = null!;
    }
}
