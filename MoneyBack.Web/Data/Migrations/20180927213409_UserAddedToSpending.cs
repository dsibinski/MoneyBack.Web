using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyBack.Web.Data.Migrations
{
    public partial class UserAddedToSpending : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Spendings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Spendings_UserId",
                table: "Spendings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spendings_AspNetUsers_UserId",
                table: "Spendings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spendings_AspNetUsers_UserId",
                table: "Spendings");

            migrationBuilder.DropIndex(
                name: "IX_Spendings_UserId",
                table: "Spendings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Spendings");
        }
    }
}
