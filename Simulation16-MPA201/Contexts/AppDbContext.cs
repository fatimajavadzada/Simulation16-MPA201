using Microsoft.EntityFrameworkCore;
using Simulation16_MPA201.Models;

namespace Simulation16_MPA201.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Branch> Branches { get; set; }
    }
}
