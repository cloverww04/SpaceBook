using Microsoft.EntityFrameworkCore;
using SpaceBook.Models;
namespace SpaceBook

{

    public class SpaceBookDbContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<SpaceObject>? SpaceObjects { get; set; }
        public DbSet<SpaceObjectContent>? SpaceObjectsContent { get; set; }
        public DbSet<UserGeneratedSpaceContent>? UsersGeneratedSpaceContent { get; set; }
        public DbSet<Comment>? Comments { get; set; }

        public SpaceBookDbContext(DbContextOptions<SpaceBookDbContext> context) : base(context) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<User>().HasData(new User { UserId = 1, FirstName = "Nathan", LastName = "Clover" });
            modelBuilder.Entity<User>().HasData(new User { UserId = 2, FirstName = "Bob", LastName = "Johnson" });

            modelBuilder.Entity<SpaceObject>().HasData(new SpaceObject { SpaceObjectId = 1, Name = "Planet X", Description = "A mysterious planet" });
            modelBuilder.Entity<SpaceObject>().HasData(new SpaceObject { SpaceObjectId = 2, Name = "Comet Y", Description = "A fast-moving comet" });

            modelBuilder.Entity<SpaceObjectContent>().HasData(new SpaceObjectContent { SpaceObjectContentId = 1, SpaceObjectId = 1, ContentId = 1 });
            modelBuilder.Entity<SpaceObjectContent>().HasData(new SpaceObjectContent { SpaceObjectContentId = 2, SpaceObjectId = 2, ContentId = 2 });

            modelBuilder.Entity<UserGeneratedSpaceContent>().HasData(new UserGeneratedSpaceContent
            {
                ContentId = 1,
                Title = "User-Generated Content 1",
                Description = "Description of user-generated content 1",
                Type = ContentType.SpaceFact,
                UserId = 1
            });

            modelBuilder.Entity<UserGeneratedSpaceContent>().HasData(new UserGeneratedSpaceContent
            {
                ContentId = 2,
                Title = "User-Generated Content 2",
                Description = "Description of user-generated content 2",
                Type = ContentType.SpaceMission,
                UserId = 2
            });

            modelBuilder.Entity<SpaceObjectContent>()
                .HasOne(so => so.Content)
                .WithMany(ugsc => ugsc.AssociatedSpaceObjects)
                .HasForeignKey(so => so.ContentId);
        }

    }
}

