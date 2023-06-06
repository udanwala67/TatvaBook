using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TatvaBook.Entities.Migrations
{
    public partial class Story : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "url",
                table: "Stories",
                newName: "Url");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Stories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Stories",
                newName: "url");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Stories",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
