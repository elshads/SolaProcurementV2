using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolaProcurementV2.Server.Migrations
{
    public partial class Migration03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LineNo",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReturnMessage",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RowIndex",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LineNo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReturnMessage",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RowIndex",
                table: "AspNetUsers");
        }
    }
}
