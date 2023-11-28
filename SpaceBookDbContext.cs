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
        public DbSet<ContentType>? ContentTypes { get; set; }

        public SpaceBookDbContext(DbContextOptions<SpaceBookDbContext> context) : base(context) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<User>().HasData(new User { UserId = 1, FirstName = "Nathan", LastName = "Clover" });
            modelBuilder.Entity<User>().HasData(new User { UserId = 2, FirstName = "Bob", LastName = "Johnson" });

            modelBuilder.Entity<SpaceObject>().HasData(new SpaceObject { SpaceObjectId = 1, Name = "Planets", Description = "This is information related to planets" });
            modelBuilder.Entity<SpaceObject>().HasData(new SpaceObject { SpaceObjectId = 2, Name = "Stars", Description = "This is information related to stars" });
            modelBuilder.Entity<SpaceObject>().HasData(new SpaceObject { SpaceObjectId = 3, Name = "Galaxy", Description = "This is information related to different galaxies" });

            modelBuilder.Entity<SpaceObjectContent>().HasData(new SpaceObjectContent { SpaceObjectContentId = 1, SpaceObjectId = 1, ContentId = 1 });
            modelBuilder.Entity<SpaceObjectContent>().HasData(new SpaceObjectContent { SpaceObjectContentId = 2, SpaceObjectId = 2, ContentId = 2 });

            modelBuilder.Entity<UserGeneratedSpaceContent>().HasData(new UserGeneratedSpaceContent
            {
                ContentId = 1,
                Title = "User-Generated Content 1",
                Description = "Description of user-generated content 1",
                TypeId = 2,
                UserId = 1,
            });

            modelBuilder.Entity<UserGeneratedSpaceContent>().HasData(new UserGeneratedSpaceContent
            {
                ContentId = 2,
                Title = "User-Generated Content 2",
                Description = "Description of user-generated content 2",
                TypeId = 1,
                UserId = 2,
            });

            modelBuilder.Entity<ContentType>().HasData(new ContentType { Id = 1, Type = "Space Fact" });
            modelBuilder.Entity<ContentType>().HasData(new ContentType { Id = 2, Type = "Space Mission" });
            modelBuilder.Entity<ContentType>().HasData(new ContentType { Id = 3, Type = "Event" });

            modelBuilder.Entity<SpaceObjectContent>()
                .HasOne(so => so.Content)
                .WithMany(ugsc => ugsc.SpaceObjectContents)
                .HasForeignKey(so => so.ContentId);

            modelBuilder.Entity<UserGeneratedSpaceContent>()
                .HasOne(ugsc => ugsc.Type)
                .WithMany()
                .HasForeignKey(ugsc => ugsc.TypeId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.UserGeneratedSpaceContent)
                .WithMany(ugsc => ugsc.Comments)
                .HasForeignKey(c => c.UserGeneratedSpaceContentId)
                .OnDelete(DeleteBehavior.Cascade);


        }

    }
}

