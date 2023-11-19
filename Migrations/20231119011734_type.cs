using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceBook.Migrations
{
    public partial class type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersGeneratedSpaceContent_TypeId",
                table: "UsersGeneratedSpaceContent");

            migrationBuilder.AddColumn<int>(
                name: "UserGeneratedSpaceContentContentId",
                table: "ContentTypes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersGeneratedSpaceContent_TypeId",
                table: "UsersGeneratedSpaceContent",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentTypes_UserGeneratedSpaceContentContentId",
                table: "ContentTypes",
                column: "UserGeneratedSpaceContentContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentTypes_UsersGeneratedSpaceContent_UserGeneratedSpaceC~",
                table: "ContentTypes",
                column: "UserGeneratedSpaceContentContentId",
                principalTable: "UsersGeneratedSpaceContent",
                principalColumn: "ContentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentTypes_UsersGeneratedSpaceContent_UserGeneratedSpaceC~",
                table: "ContentTypes");

            migrationBuilder.DropIndex(
                name: "IX_UsersGeneratedSpaceContent_TypeId",
                table: "UsersGeneratedSpaceContent");

            migrationBuilder.DropIndex(
                name: "IX_ContentTypes_UserGeneratedSpaceContentContentId",
                table: "ContentTypes");

            migrationBuilder.DropColumn(
                name: "UserGeneratedSpaceContentContentId",
                table: "ContentTypes");

            migrationBuilder.CreateIndex(
                name: "IX_UsersGeneratedSpaceContent_TypeId",
                table: "UsersGeneratedSpaceContent",
                column: "TypeId");
        }
    }
}
