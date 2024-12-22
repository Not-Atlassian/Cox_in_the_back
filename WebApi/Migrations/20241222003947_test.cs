using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Adjustments");

            migrationBuilder.AddColumn<string>(
                name: "reason",
                table: "Adjustments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reason",
                table: "Adjustments");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "Adjustments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
