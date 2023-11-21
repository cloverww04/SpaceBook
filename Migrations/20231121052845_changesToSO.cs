using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceBook.Migrations
{
    public partial class changesToSO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersGeneratedSpaceContent_SpaceObjects_SpaceObjectId",
                table: "UsersGeneratedSpaceContent");

            migrationBuilder.AlterColumn<int>(
                name: "SpaceObjectId",
                table: "UsersGeneratedSpaceContent",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "UsersGeneratedSpaceContent",
                keyColumn: "ContentId",
                keyValue: 1,
                column: "SpaceObjectId",
                value: null);

            migrationBuilder.UpdateData(
                table: "UsersGeneratedSpaceContent",
                keyColumn: "ContentId",
                keyValue: 2,
                column: "SpaceObjectId",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersGeneratedSpaceContent_SpaceObjects_SpaceObjectId",
                table: "UsersGeneratedSpaceContent",
                column: "SpaceObjectId",
                principalTable: "SpaceObjects",
                principalColumn: "SpaceObjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersGeneratedSpaceContent_SpaceObjects_SpaceObjectId",
                table: "UsersGeneratedSpaceContent");

            migrationBuilder.AlterColumn<int>(
                name: "SpaceObjectId",
                table: "UsersGeneratedSpaceContent",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "UsersGeneratedSpaceContent",
                keyColumn: "ContentId",
                keyValue: 1,
                column: "SpaceObjectId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "UsersGeneratedSpaceContent",
                keyColumn: "ContentId",
                keyValue: 2,
                column: "SpaceObjectId",
                value: 2);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersGeneratedSpaceContent_SpaceObjects_SpaceObjectId",
                table: "UsersGeneratedSpaceContent",
                column: "SpaceObjectId",
                principalTable: "SpaceObjects",
                principalColumn: "SpaceObjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
