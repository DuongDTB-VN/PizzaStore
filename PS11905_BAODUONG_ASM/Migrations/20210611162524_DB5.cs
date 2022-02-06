using Microsoft.EntityFrameworkCore.Migrations;

namespace PS11905_BAODUONG_ASM.Migrations
{
    public partial class DB5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Trangthai",
                table: "Donhangs",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Trangthai",
                table: "Donhangs",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
