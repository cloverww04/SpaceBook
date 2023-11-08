﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SpaceBook;

#nullable disable

namespace SpaceBook.Migrations
{
    [DbContext(typeof(SpaceBookDbContext))]
    [Migration("20231108030234_initialCreate")]
    partial class initialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SpaceBook.Models.SpaceObject", b =>
                {
                    b.Property<int>("SpaceObjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SpaceObjectId"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("SpaceObjectId");

                    b.ToTable("SpaceObjects");

                    b.HasData(
                        new
                        {
                            SpaceObjectId = 1,
                            Description = "A mysterious planet",
                            Name = "Planet X"
                        },
                        new
                        {
                            SpaceObjectId = 2,
                            Description = "A fast-moving comet",
                            Name = "Comet Y"
                        });
                });

            modelBuilder.Entity("SpaceBook.Models.SpaceObjectContent", b =>
                {
                    b.Property<int>("SpaceObjectContentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SpaceObjectContentId"));

                    b.Property<int>("ContentId")
                        .HasColumnType("integer");

                    b.Property<int>("SpaceObjectId")
                        .HasColumnType("integer");

                    b.HasKey("SpaceObjectContentId");

                    b.HasIndex("ContentId");

                    b.HasIndex("SpaceObjectId");

                    b.ToTable("SpaceObjectsContent");

                    b.HasData(
                        new
                        {
                            SpaceObjectContentId = 1,
                            ContentId = 1,
                            SpaceObjectId = 1
                        },
                        new
                        {
                            SpaceObjectContentId = 2,
                            ContentId = 2,
                            SpaceObjectId = 2
                        });
                });

            modelBuilder.Entity("SpaceBook.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("UID")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            FirstName = "Nathan",
                            LastName = "Clover"
                        },
                        new
                        {
                            UserId = 2,
                            FirstName = "Bob",
                            LastName = "Johnson"
                        });
                });

            modelBuilder.Entity("SpaceBook.Models.UserGeneratedSpaceContent", b =>
                {
                    b.Property<int>("ContentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ContentId"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("ContentId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersGeneratedSpaceContent");

                    b.HasData(
                        new
                        {
                            ContentId = 1,
                            Description = "Description of user-generated content 1",
                            Title = "User-Generated Content 1",
                            Type = 0,
                            UserId = 1
                        },
                        new
                        {
                            ContentId = 2,
                            Description = "Description of user-generated content 2",
                            Title = "User-Generated Content 2",
                            Type = 1,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("SpaceBook.Models.SpaceObjectContent", b =>
                {
                    b.HasOne("SpaceBook.Models.UserGeneratedSpaceContent", "Content")
                        .WithMany("AssociatedSpaceObjects")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpaceBook.Models.SpaceObject", "SpaceObject")
                        .WithMany("AssociatedSpaceContent")
                        .HasForeignKey("SpaceObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");

                    b.Navigation("SpaceObject");
                });

            modelBuilder.Entity("SpaceBook.Models.UserGeneratedSpaceContent", b =>
                {
                    b.HasOne("SpaceBook.Models.User", "User")
                        .WithMany("CreatedSpaceContent")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SpaceBook.Models.SpaceObject", b =>
                {
                    b.Navigation("AssociatedSpaceContent");
                });

            modelBuilder.Entity("SpaceBook.Models.User", b =>
                {
                    b.Navigation("CreatedSpaceContent");
                });

            modelBuilder.Entity("SpaceBook.Models.UserGeneratedSpaceContent", b =>
                {
                    b.Navigation("AssociatedSpaceObjects");
                });
#pragma warning restore 612, 618
        }
    }
}
