using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinhaSaudeFeminina.Migrations
{
    /// <inheritdoc />
    public partial class AppDbContextUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagSymptoms_Symptoms_SymptomId",
                table: "TagSymptoms");

            migrationBuilder.AddForeignKey(
                name: "FK_TagSymptoms_Symptoms_SymptomId",
                table: "TagSymptoms",
                column: "SymptomId",
                principalTable: "Symptoms",
                principalColumn: "SymptomId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagSymptoms_Symptoms_SymptomId",
                table: "TagSymptoms");

            migrationBuilder.AddForeignKey(
                name: "FK_TagSymptoms_Symptoms_SymptomId",
                table: "TagSymptoms",
                column: "SymptomId",
                principalTable: "Symptoms",
                principalColumn: "SymptomId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
