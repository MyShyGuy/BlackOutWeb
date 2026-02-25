using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blackout.Models.Data.Migrations
{
    /// <inheritdoc />
    public partial class aditEAN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "EAN",
                schema: "prd",
                table: "Products",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 200);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EAN",
                schema: "prd",
                table: "Products",
                type: "int",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
