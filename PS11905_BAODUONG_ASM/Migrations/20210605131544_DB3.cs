using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PS11905_BAODUONG_ASM.Migrations
{
    public partial class DB3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Khachhangs",
                columns: table => new
                {
                    KhachhangID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(maxLength: 150, nullable: false),
                    Ngaysinh = table.Column<DateTime>(nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ConfirmPassword = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Mota = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khachhangs", x => x.KhachhangID);
                });

            migrationBuilder.CreateTable(
                name: "Donhangs",
                columns: table => new
                {
                    DonhangID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhachhangID = table.Column<int>(nullable: false),
                    Ngaydat = table.Column<DateTime>(nullable: false),
                    DiaChi = table.Column<string>(maxLength: 250, nullable: false),
                    Tongtien = table.Column<double>(nullable: false),
                    Trangthai = table.Column<bool>(nullable: false),
                    Ghichu = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donhangs", x => x.DonhangID);
                    table.ForeignKey(
                        name: "FK_Donhangs_Khachhangs_KhachhangID",
                        column: x => x.KhachhangID,
                        principalTable: "Khachhangs",
                        principalColumn: "KhachhangID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonhangChitiets",
                columns: table => new
                {
                    ChitietID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonhangID = table.Column<int>(nullable: false),
                    MonAnID = table.Column<int>(nullable: false),
                    Soluong = table.Column<int>(nullable: false),
                    Thanhtien = table.Column<double>(nullable: false),
                    Ghichu = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonhangChitiets", x => x.ChitietID);
                    table.ForeignKey(
                        name: "FK_DonhangChitiets_Donhangs_DonhangID",
                        column: x => x.DonhangID,
                        principalTable: "Donhangs",
                        principalColumn: "DonhangID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonhangChitiets_MonAns_MonAnID",
                        column: x => x.MonAnID,
                        principalTable: "MonAns",
                        principalColumn: "MonAnID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonhangChitiets_DonhangID",
                table: "DonhangChitiets",
                column: "DonhangID");

            migrationBuilder.CreateIndex(
                name: "IX_DonhangChitiets_MonAnID",
                table: "DonhangChitiets",
                column: "MonAnID");

            migrationBuilder.CreateIndex(
                name: "IX_Donhangs_KhachhangID",
                table: "Donhangs",
                column: "KhachhangID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonhangChitiets");

            migrationBuilder.DropTable(
                name: "Donhangs");

            migrationBuilder.DropTable(
                name: "Khachhangs");
        }
    }
}
