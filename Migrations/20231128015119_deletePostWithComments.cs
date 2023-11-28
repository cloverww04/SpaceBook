using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceBook.Migrations
{
    public partial class deletePostWithComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_UsersGeneratedSpaceContent_UserGeneratedSpaceConte~",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserGeneratedSpaceContentContentId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserGeneratedSpaceContentContentId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "UserGeneratedSpaceContentId",
                table: "Comments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserGeneratedSpaceContentId",
                table: "Comments",
                column: "UserGeneratedSpaceContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_UsersGeneratedSpaceContent_UserGeneratedSpaceConte~",
                table: "Comments",
                column: "UserGeneratedSpaceContentId",
                principalTable: "UsersGeneratedSpaceContent",
                principalColumn: "ContentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_UsersGeneratedSpaceContent_UserGeneratedSpaceConte~",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserGeneratedSpaceContentId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserGeneratedSpaceContentId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "UserGeneratedSpaceContentContentId",
                table: "Comments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserGeneratedSpaceContentContentId",
                table: "Comments",
                column: "UserGeneratedSpaceContentContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_UsersGeneratedSpaceContent_UserGeneratedSpaceConte~",
                table: "Comments",
                column: "UserGeneratedSpaceContentContentId",
                principalTable: "UsersGeneratedSpaceContent",
                principalColumn: "ContentId");
        }
    }
}
