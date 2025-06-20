using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wörter.AppData.Migrations
{
    /// <inheritdoc />
    public partial class Mig0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notizen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Slug = table.Column<string>(type: "TEXT", nullable: false),
                    TextContent = table.Column<string>(type: "TEXT", nullable: false),
                    ReferenceLink = table.Column<string>(type: "TEXT", nullable: true),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Tags = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notizen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wörter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Art = table.Column<int>(type: "INTEGER", nullable: false),
                    DE = table.Column<string>(type: "TEXT", nullable: false),
                    EN = table.Column<string>(type: "TEXT", nullable: true),
                    TR = table.Column<string>(type: "TEXT", nullable: true),
                    Geschlecht = table.Column<string>(type: "TEXT", nullable: true),
                    Aussprache = table.Column<string>(type: "TEXT", nullable: true),
                    Plural = table.Column<string>(type: "TEXT", nullable: true),
                    Verbkonjugation = table.Column<string>(type: "TEXT", nullable: true),
                    Beispiel = table.Column<string>(type: "TEXT", nullable: true),
                    Erläuterung = table.Column<string>(type: "TEXT", nullable: true),
                    Synonym = table.Column<string>(type: "TEXT", nullable: true),
                    Gegenteil = table.Column<string>(type: "TEXT", nullable: true),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Tags = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wörter", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wörter_DE_Art",
                table: "Wörter",
                columns: new[] { "DE", "Art" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notizen");

            migrationBuilder.DropTable(
                name: "Wörter");
        }
    }
}
