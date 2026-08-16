using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddCapacityAndNumberFieldsToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfChairs",
                table: "Tables",
                newName: "Number");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Tables",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tables",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Tables");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Tables",
                newName: "NumberOfChairs");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Tables",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tables",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
