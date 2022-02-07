using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolaProcurementV2.Server.Migrations
{
    public partial class Migration_202112282 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastLogin",
                table: "AspNetUsers",
                newName: "LastActivity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastActivity",
                table: "AspNetUsers",
                newName: "LastLogin");
        }
    }
}
