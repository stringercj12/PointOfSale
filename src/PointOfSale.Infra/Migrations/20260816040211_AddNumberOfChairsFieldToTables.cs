using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddNumberOfChairsFieldToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Tables",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NumberOfChairs",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Tables",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "NumberOfChairs",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Tables");
        }
    }
}
