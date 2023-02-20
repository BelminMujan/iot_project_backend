using IOT_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace IOT_Backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Room> Room { get; set; }
    }
}