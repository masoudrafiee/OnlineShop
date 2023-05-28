using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Infrastructure.Migrations
{
    public partial class Init01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "DiscountType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    DiscountValue = table.Column<decimal>(nullable: false),
                    Profit = table.Column<decimal>(nullable: false),
                    ProductType = table.Column<int>(type: "int", nullable: true),
                    DiscountType = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buyer",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityGuid = table.Column<string>(maxLength: 200, nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Basket",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Basket_Buyer_BuyerId",
                        column: x => x.BuyerId,
                        principalSchema: "dbo",
                        principalTable: "Buyer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    Address_Street = table.Column<string>(nullable: true),
                    Address_City = table.Column<string>(nullable: true),
                    Address_State = table.Column<string>(nullable: true),
                    Address_Country = table.Column<string>(nullable: true),
                    Address_ZipCode = table.Column<string>(nullable: true),
                    Total = table.Column<decimal>(nullable: false),
                    ShippingType = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(nullable: true),
                    BuyerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Buyer_BuyerId",
                        column: x => x.BuyerId,
                        principalSchema: "dbo",
                        principalTable: "Buyer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BasketItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    ProductType = table.Column<int>(type: "int", nullable: true),
                    BasketId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Profit = table.Column<decimal>(nullable: false),
                    DiscountType = table.Column<int>(type: "int", nullable: true),
                    DiscountValue = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItem_Basket_BasketId",
                        column: x => x.BasketId,
                        principalSchema: "dbo",
                        principalTable: "Basket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    ProductType = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Profit = table.Column<decimal>(nullable: false),
                    DiscountValue = table.Column<decimal>(nullable: false),
                    DiscountType = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "dbo",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "DiscountType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Percentage" },
                    { 2, "Amount" }
                });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fragile" },
                    { 2, "Normal" }
                });

            migrationBuilder.InsertData(
                table: "ShippingType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Express" },
                    { 2, "Post" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItem_BasketId",
                table: "BasketItem",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Basket_BuyerId",
                schema: "dbo",
                table: "Basket",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Buyer_IdentityGuid",
                schema: "dbo",
                table: "Buyer",
                column: "IdentityGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_BuyerId",
                schema: "dbo",
                table: "Order",
                column: "BuyerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketItem");

            migrationBuilder.DropTable(
                name: "DiscountType");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "ProductItem");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropTable(
                name: "ShippingType");

            migrationBuilder.DropTable(
                name: "Basket",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Buyer",
                schema: "dbo");
        }
    }
}
