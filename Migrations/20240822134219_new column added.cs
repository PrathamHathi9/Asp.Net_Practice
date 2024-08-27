using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotNet.Migrations
{
    /// <inheritdoc />
    public partial class newcolumnadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "customers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_customers_ProductId",
                table: "customers",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_customers_products_ProductId",
                table: "customers",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_products_ProductId",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "IX_customers_ProductId",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "customers");
        }
    }
}
