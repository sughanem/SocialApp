using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialApp.Database;

#nullable disable

namespace SocialApp.Database
{
    public partial class SocialAppDatabase : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public SocialAppDatabase()
        {
        }

        public SocialAppDatabase(DbContextOptions<SocialAppDatabase> options)
            : base(options)
        {
        }

        public virtual DbSet<Friendship> Friendships { get; set; }
        public virtual DbSet<UserPage> UserPages { get; set; }
        public virtual DbSet<UserPost> UserPosts { get; set; }
        public virtual DbSet<FriendshipStatus> FriendshipStatuses { get; set; }
        public virtual DbSet<MyStatus> MyStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly()).Build();
                DbContextOptionsBuilder dbContextOptionsBuilder = optionsBuilder
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Friendship>().HasKey(f => new { f.RequesterId, f.AddresseeId });
            modelBuilder.Entity<FriendshipStatus>().HasKey(f => new { f.RequesterId, f.AddresseeId, f.SpecifiedDateTime });

            modelBuilder.Entity<Friendship>()
                .HasOne(pt => pt.Requster)
                .WithMany()
                .HasForeignKey(pt => pt.RequesterId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Friendship>()
                .HasOne(pt => pt.Addressee)
                .WithMany()
                .HasForeignKey(pt => pt.AddresseeId)
                .OnDelete(DeleteBehavior.NoAction);
        }       
      
    }
}
