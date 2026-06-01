using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Users_Userid",
                table: "Participants");

            migrationBuilder.RenameColumn(
                name: "Userid",
                table: "Participants",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Participants_Userid",
                table: "Participants",
                newName: "IX_Participants_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Users_UserId",
                table: "Participants",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Users_UserId",
                table: "Participants");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Participants",
                newName: "Userid");

            migrationBuilder.RenameIndex(
                name: "IX_Participants_UserId",
                table: "Participants",
                newName: "IX_Participants_Userid");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Users_Userid",
                table: "Participants",
                column: "Userid",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
