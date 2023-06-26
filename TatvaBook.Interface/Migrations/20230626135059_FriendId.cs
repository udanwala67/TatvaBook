using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TatvaBook.Entities.Migrations
{
    public partial class FriendId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Friends",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "RecieverId",
                table: "Friends",
                newName: "FriendId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Friends",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "FriendId",
                table: "Friends",
                newName: "RecieverId");
        }
    }
}
