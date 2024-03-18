using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VG_Review.Migrations
{
    public partial class adjustReviewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reviews");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
