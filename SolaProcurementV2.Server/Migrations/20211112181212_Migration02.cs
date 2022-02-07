using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolaProcurementV2.Server.Migrations
{
    public partial class Migration02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThemeId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "AspNetUsers");
        }
    }
}
