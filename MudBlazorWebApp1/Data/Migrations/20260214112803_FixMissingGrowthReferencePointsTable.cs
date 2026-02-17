using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MudBlazorWebApp1.Migrations
{
    /// <inheritdoc />
    public partial class FixMissingGrowthReferencePointsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GrowthReferencePoints",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Source = table.Column<int>(type: "int", nullable: false),
                    Metric = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    X = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    P1 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    P3 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    P5 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    P15 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    P25 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    P50 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    P75 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    P85 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    P95 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    P97 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    P99 = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrowthReferencePoints", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GrowthReferencePoints_Source_Metric_Gender_X",
                table: "GrowthReferencePoints",
                columns: new[] { "Source", "Metric", "Gender", "X" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "GrowthReferencePoints");
        }
    }
}
