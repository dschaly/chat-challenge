using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DataContext()
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<RoomAction> RoomActions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<HighFive> HighFives { get; set; }
        public DbSet<ChatAction> ChatActions { get; set; }
    }
}
