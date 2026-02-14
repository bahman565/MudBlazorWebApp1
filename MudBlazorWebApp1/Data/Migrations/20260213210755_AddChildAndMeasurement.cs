using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MudBlazorWebApp1.Migrations
{
    /// <inheritdoc />
    public partial class AddChildAndMeasurement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Children",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Children", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChildId = table.Column<int>(type: "int", nullable: false),
                    MeasuredAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WeightKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LengthCm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HeadCircumferenceCm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measurements_Children_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Children",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GrowthReferencePoints_Source_Metric_Gender_X",
                table: "GrowthReferencePoints",
                columns: new[] { "Source", "Metric", "Gender", "X" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_ChildId",
                table: "Measurements",
                column: "ChildId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrowthReferencePoints");

            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "Children");
        }
    }
}
