using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolaProcurementV2.Server.Migrations
{
    public partial class Migration_20210105 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Administrator",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Administrator",
                table: "AspNetUsers");
        }
    }
}
