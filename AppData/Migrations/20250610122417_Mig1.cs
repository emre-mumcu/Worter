using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wörter.AppData.Migrations
{
    /// <inheritdoc />
    public partial class Mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gegenteil",
                table: "Wörter",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Synonym",
                table: "Wörter",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gegenteil",
                table: "Wörter");

            migrationBuilder.DropColumn(
                name: "Synonym",
                table: "Wörter");
        }
    }
}
