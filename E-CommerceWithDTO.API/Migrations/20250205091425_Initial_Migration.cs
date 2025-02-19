using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_CommerceWithDTO.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ProductCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Devices and gadgets", "Electronics" },
                    { 2, "Printed and digital books", "Books" },
                    { 3, "Apparel and accessories", "Clothing" },
                    { 4, "Kitchen and home utilities", "Home Appliances" },
                    { 5, "Toys and games for kids", "Toys" }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "CustomerName", "OrderDate" },
                values: new object[,]
                {
                    { 1, "Alice Johnson", new DateTime(2025, 1, 21, 17, 14, 25, 284, DateTimeKind.Local).AddTicks(5073) },
                    { 2, "Bob Smith", new DateTime(2025, 1, 23, 17, 14, 25, 284, DateTimeKind.Local).AddTicks(5093) },
                    { 3, "Charlie Davis", new DateTime(2025, 1, 26, 17, 14, 25, 284, DateTimeKind.Local).AddTicks(5095) },
                    { 4, "David Wilson", new DateTime(2025, 1, 27, 17, 14, 25, 284, DateTimeKind.Local).AddTicks(5097) },
                    { 5, "Eva Roberts", new DateTime(2025, 1, 29, 17, 14, 25, 284, DateTimeKind.Local).AddTicks(5098) },
                    { 6, "Fay Green", new DateTime(2025, 1, 31, 17, 14, 25, 284, DateTimeKind.Local).AddTicks(5100) },
                    { 7, "George Turner", new DateTime(2025, 2, 2, 17, 14, 25, 284, DateTimeKind.Local).AddTicks(5101) },
                    { 8, "Helen Adams", new DateTime(2025, 2, 3, 17, 14, 25, 284, DateTimeKind.Local).AddTicks(5103) },
                    { 9, "Ivy Lee", new DateTime(2025, 2, 4, 17, 14, 25, 284, DateTimeKind.Local).AddTicks(5104) },
                    { 10, "James Clark", new DateTime(2025, 2, 5, 17, 14, 25, 284, DateTimeKind.Local).AddTicks(5105) }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "Latest model smartphone", "Smartphone", 699.99m, 50 },
                    { 2, "High performance laptop", "Laptop", 1299.99m, 30 },
                    { 3, "Bestselling fiction novel", "Fiction Novel", 19.99m, 100 },
                    { 4, "100% cotton t-shirt", "T-Shirt", 15.99m, 200 },
                    { 5, "High-speed kitchen blender", "Blender", 89.99m, 40 }
                });

            migrationBuilder.InsertData(
                table: "OrderItem",
                columns: new[] { "Id", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 699.99m },
                    { 2, 1, 3, 2, 19.99m },
                    { 3, 2, 2, 1, 1299.99m },
                    { 4, 2, 4, 3, 15.99m },
                    { 5, 3, 5, 1, 89.99m },
                    { 6, 4, 1, 1, 699.99m },
                    { 7, 5, 2, 2, 1299.99m },
                    { 8, 6, 4, 4, 15.99m },
                    { 9, 7, 3, 5, 19.99m },
                    { 10, 8, 5, 3, 89.99m }
                });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 3, 4 },
                    { 4, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_CategoryId",
                table: "ProductCategory",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
