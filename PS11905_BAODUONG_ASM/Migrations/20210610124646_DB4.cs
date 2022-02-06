using Microsoft.EntityFrameworkCore.Migrations;

namespace PS11905_BAODUONG_ASM.Migrations
{
    public partial class DB4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Khachhangs",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Khachhangs");
        }
    }
}
