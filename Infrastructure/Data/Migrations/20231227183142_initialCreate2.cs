using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductBrands_ProductBrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "ProductsBrand");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductTypeId",
                table: "ProductsBrand",
                newName: "IX_ProductsBrand_ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductBrandId",
                table: "ProductsBrand",
                newName: "IX_ProductsBrand_ProductBrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsBrand",
                table: "ProductsBrand",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsBrand_ProductBrands_ProductBrandId",
                table: "ProductsBrand",
                column: "ProductBrandId",
                principalTable: "ProductBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsBrand_ProductTypes_ProductTypeId",
                table: "ProductsBrand",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsBrand_ProductBrands_ProductBrandId",
                table: "ProductsBrand");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsBrand_ProductTypes_ProductTypeId",
                table: "ProductsBrand");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsBrand",
                table: "ProductsBrand");

            migrationBuilder.RenameTable(
                name: "ProductsBrand",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsBrand_ProductTypeId",
                table: "Products",
                newName: "IX_Products_ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsBrand_ProductBrandId",
                table: "Products",
                newName: "IX_Products_ProductBrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductBrands_ProductBrandId",
                table: "Products",
                column: "ProductBrandId",
                principalTable: "ProductBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
