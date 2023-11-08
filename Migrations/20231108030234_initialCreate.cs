using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SpaceBook.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Type = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersGeneratedSpaceContent", x => x.ContentId);
                    table.ForeignKey(
                        name: "FK_UsersGeneratedSpaceContent_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
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
                columns: new[] { "ContentId", "Description", "Title", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, "Description of user-generated content 1", "User-Generated Content 1", 0, 1 },
                    { 2, "Description of user-generated content 2", "User-Generated Content 2", 1, 2 }
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
                name: "IX_SpaceObjectsContent_ContentId",
                table: "SpaceObjectsContent",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceObjectsContent_SpaceObjectId",
                table: "SpaceObjectsContent",
                column: "SpaceObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersGeneratedSpaceContent_UserId",
                table: "UsersGeneratedSpaceContent",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpaceObjectsContent");

            migrationBuilder.DropTable(
                name: "SpaceObjects");

            migrationBuilder.DropTable(
                name: "UsersGeneratedSpaceContent");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
