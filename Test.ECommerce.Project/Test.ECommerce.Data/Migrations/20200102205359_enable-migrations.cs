using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Test.ECommerce.Data.Migrations
{
    public partial class enablemigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryCode = table.Column<string>(nullable: false),
                    CategoryName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryCode);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductCode = table.Column<string>(nullable: false),
                    CategoryCode = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ProductPrice = table.Column<double>(nullable: false),
                    StockCount = table.Column<long>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductCode);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryCode",
                        column: x => x.CategoryCode,
                        principalTable: "Categories",
                        principalColumn: "CategoryCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryCode",
                table: "Products",
                column: "CategoryCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
