using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VG_Review.Migrations
{
    public partial class revertedBackReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_userId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_userId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Reviews");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_userId",
                table: "Reviews",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_userId",
                table: "Reviews",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
