using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalGameAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlayerPosition_X = table.Column<float>(type: "real", nullable: true),
                    PlayerPosition_Y = table.Column<float>(type: "real", nullable: true),
                    PlayerPosition_Z = table.Column<float>(type: "real", nullable: true),
                    CoinPosition_X = table.Column<float>(type: "real", nullable: true),
                    CoinPosition_Y = table.Column<float>(type: "real", nullable: true),
                    CoinPosition_Z = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
