using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddTableProductAttachment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAttachments");

            migrationBuilder.AddColumn<Guid>(
                name: "AttachmentId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductAttachment",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttachment", x => new { x.ProductId, x.AttachmentId });
                    table.ForeignKey(
                        name: "FK_ProductAttachment_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductAttachment_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_AttachmentId",
                table: "Products",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttachment_AttachmentId",
                table: "ProductAttachment",
                column: "AttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Attachments_AttachmentId",
                table: "Products",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Attachments_AttachmentId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductAttachment");

            migrationBuilder.DropIndex(
                name: "IX_Products_AttachmentId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ProductAttachments",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttachments", x => new { x.ProductId, x.AttachmentId });
                    table.ForeignKey(
                        name: "FK_ProductAttachments_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAttachments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttachments_AttachmentId",
                table: "ProductAttachments",
                column: "AttachmentId");
        }
    }
}
