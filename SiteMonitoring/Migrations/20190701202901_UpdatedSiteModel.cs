using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteMonitoring.Migrations
{
    public partial class UpdatedSiteModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusCode",
                table: "Sites");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Sites",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Sites");

            migrationBuilder.AddColumn<int>(
                name: "StatusCode",
                table: "Sites",
                nullable: false,
                defaultValue: 0);
        }
    }
}
