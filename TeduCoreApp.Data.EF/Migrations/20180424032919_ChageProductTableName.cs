using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TeduCoreApp.Data.EF.Migrations
{
    public partial class ChageProductTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Procduct_ProductCategory_CategoryId",
                table: "Procduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_Procduct_ProductId",
                table: "ProductColor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Procduct_ProductId",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductQuantity_Procduct_ProductId",
                table: "ProductQuantity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Procduct_ProductId",
                table: "ProductTags");

            migrationBuilder.DropForeignKey(
                name: "FK_WholePrice_Procduct_ProductId",
                table: "WholePrice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Procduct",
                table: "Procduct");

            migrationBuilder.RenameTable(
                name: "Procduct",
                newName: "Product");

            migrationBuilder.RenameIndex(
                name: "IX_Procduct_CategoryId",
                table: "Product",
                newName: "IX_Product_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductCategory_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "ProductCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_Product_ProductId",
                table: "ProductColor",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                table: "ProductImage",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductQuantity_Product_ProductId",
                table: "ProductQuantity",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_Product_ProductId",
                table: "ProductTags",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WholePrice_Product_ProductId",
                table: "WholePrice",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductCategory_CategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_Product_ProductId",
                table: "ProductColor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductQuantity_Product_ProductId",
                table: "ProductQuantity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Product_ProductId",
                table: "ProductTags");

            migrationBuilder.DropForeignKey(
                name: "FK_WholePrice_Product_ProductId",
                table: "WholePrice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Procduct");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryId",
                table: "Procduct",
                newName: "IX_Procduct_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Procduct",
                table: "Procduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Procduct_ProductCategory_CategoryId",
                table: "Procduct",
                column: "CategoryId",
                principalTable: "ProductCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_Procduct_ProductId",
                table: "ProductColor",
                column: "ProductId",
                principalTable: "Procduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Procduct_ProductId",
                table: "ProductImage",
                column: "ProductId",
                principalTable: "Procduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductQuantity_Procduct_ProductId",
                table: "ProductQuantity",
                column: "ProductId",
                principalTable: "Procduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_Procduct_ProductId",
                table: "ProductTags",
                column: "ProductId",
                principalTable: "Procduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WholePrice_Procduct_ProductId",
                table: "WholePrice",
                column: "ProductId",
                principalTable: "Procduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
