using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SpaceBook.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpaceObjects",
                columns: table => new
                {
                    SpaceObjectId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceObjects", x => x.SpaceObjectId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    UID = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UsersGeneratedSpaceContent",
                columns: table => new
                {
                    ContentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SpaceObjectId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersGeneratedSpaceContent", x => x.ContentId);
                    table.ForeignKey(
                        name: "FK_UsersGeneratedSpaceContent_ContentTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ContentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersGeneratedSpaceContent_SpaceObjects_SpaceObjectId",
                        column: x => x.SpaceObjectId,
                        principalTable: "SpaceObjects",
                        principalColumn: "SpaceObjectId");
                    table.ForeignKey(
                        name: "FK_UsersGeneratedSpaceContent_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UserGeneratedSpaceContentContentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_UsersGeneratedSpaceContent_UserGeneratedSpaceConte~",
                        column: x => x.UserGeneratedSpaceContentContentId,
                        principalTable: "UsersGeneratedSpaceContent",
                        principalColumn: "ContentId");
                });

            migrationBuilder.CreateTable(
                name: "SpaceObjectsContent",
                columns: table => new
                {
                    SpaceObjectContentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SpaceObjectId = table.Column<int>(type: "integer", nullable: false),
                    ContentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceObjectsContent", x => x.SpaceObjectContentId);
                    table.ForeignKey(
                        name: "FK_SpaceObjectsContent_SpaceObjects_SpaceObjectId",
                        column: x => x.SpaceObjectId,
                        principalTable: "SpaceObjects",
                        principalColumn: "SpaceObjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpaceObjectsContent_UsersGeneratedSpaceContent_ContentId",
                        column: x => x.ContentId,
                        principalTable: "UsersGeneratedSpaceContent",
                        principalColumn: "ContentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ContentTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Space Fact" },
                    { 2, "Space Mission" },
                    { 3, "Event" }
                });

            migrationBuilder.InsertData(
                table: "SpaceObjects",
                columns: new[] { "SpaceObjectId", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "A mysterious planet", null, "Planet X" },
                    { 2, "A fast-moving comet", null, "Comet Y" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "FirstName", "LastName", "UID" },
                values: new object[,]
                {
                    { 1, "Nathan", "Clover", null },
                    { 2, "Bob", "Johnson", null }
                });

            migrationBuilder.InsertData(
                table: "UsersGeneratedSpaceContent",
                columns: new[] { "ContentId", "CreatedOn", "Description", "SpaceObjectId", "Title", "TypeId", "UserId" },
                values: new object[,]
                {
                    { 1, null, "Description of user-generated content 1", null, "User-Generated Content 1", 2, 1 },
                    { 2, null, "Description of user-generated content 2", null, "User-Generated Content 2", 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "SpaceObjectsContent",
                columns: new[] { "SpaceObjectContentId", "ContentId", "SpaceObjectId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserGeneratedSpaceContentContentId",
                table: "Comments",
                column: "UserGeneratedSpaceContentContentId");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceObjectsContent_ContentId",
                table: "SpaceObjectsContent",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceObjectsContent_SpaceObjectId",
                table: "SpaceObjectsContent",
                column: "SpaceObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersGeneratedSpaceContent_SpaceObjectId",
                table: "UsersGeneratedSpaceContent",
                column: "SpaceObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersGeneratedSpaceContent_TypeId",
                table: "UsersGeneratedSpaceContent",
                column: "TypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersGeneratedSpaceContent_UserId",
                table: "UsersGeneratedSpaceContent",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "SpaceObjectsContent");

            migrationBuilder.DropTable(
                name: "UsersGeneratedSpaceContent");

            migrationBuilder.DropTable(
                name: "ContentTypes");

            migrationBuilder.DropTable(
                name: "SpaceObjects");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
