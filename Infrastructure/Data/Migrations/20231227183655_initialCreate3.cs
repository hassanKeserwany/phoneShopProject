using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "Product");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsBrand_ProductTypeId",
                table: "Product",
                newName: "IX_Product_ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsBrand_ProductBrandId",
                table: "Product",
                newName: "IX_Product_ProductBrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductBrands_ProductBrandId",
                table: "Product",
                column: "ProductBrandId",
                principalTable: "ProductBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductTypes_ProductTypeId",
                table: "Product",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductBrands_ProductBrandId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductTypes_ProductTypeId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "ProductsBrand");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductTypeId",
                table: "ProductsBrand",
                newName: "IX_ProductsBrand_ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductBrandId",
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
    }
}
