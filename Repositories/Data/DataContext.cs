using Domain.Entities;
using Domain.Enums;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RoomAction>()
                .HasOne(p => p.User)
                .WithMany(p => p.RoomActions)
                .HasForeignKey(p => p.UserId);

            builder.Entity<RoomAction>()
                .HasOne(p => p.Comment)
                .WithOne(p => p.RoomAction);
            //.HasForeignKey(p => p.)

            builder.Entity<RoomAction>()
                .HasOne(p => p.HighFive)
                .WithOne(p => p.RoomAction);

            builder.Entity<Comment>()
                .HasOne(p => p.User)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.UserId);

            builder.Entity<HighFive>()
                .HasOne(p => p.User)
                .WithMany(p => p.HighFives)
                .HasForeignKey(p => p.UserIdTo);
        }

        public void SeedData()
        {
            var roomActions = new List<RoomAction>
                {
                    new RoomAction
                    {
                        Id = 1,
                        ActionId = (int)ActionEnum.ENTER_THE_ROOM,
                        UserId = 1,
                        CommentId = null,
                        HighFiveId = null,
                        ActionDate = new DateTime(2022, 11, 26, 5, 0, 0),
                        User = new User
                        {
                            Id = 1,
                            UserName = "Bob"
                        },
                        Comment = null,
                        HighFive = null
                    },
                    new RoomAction
                    {
                        Id = 2,
                        ActionId = (int)ActionEnum.COMMENT,
                        UserId = 2,
                        CommentId = null,
                        HighFiveId = null,
                        ActionDate = new DateTime(2022, 11, 26, 5, 5, 0),
                        User = new User
                        {
                            Id = 2,
                            UserName = "Kate"
                        },
                        Comment = new Comment
                        {
                            Id = 1,
                            Message = "TEST",
                            UserId = 2,
                        },
                        HighFive = null
                    },
                    new RoomAction
                    {
                        Id = 3,
                        ActionId = (int)ActionEnum.HIGH_FIVE,
                        UserId = 2,
                        CommentId = null,
                        HighFiveId = null,
                        ActionDate = new DateTime(2022, 11, 26, 5, 5, 0),
                        User = null,
                        Comment = null,
                        HighFive = new HighFive
                        {
                            Id = 1,
                            UserIdTo = 1,
                        }
                    }
                };
            RoomActions.AddRange(roomActions);
            SaveChanges();
        }
    }
}
