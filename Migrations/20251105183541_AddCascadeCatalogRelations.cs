using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinhaSaudeFeminina.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeCatalogRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileGenders_Genders_GenderId",
                table: "ProfileGenders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileObjectives_Objectives_ObjectiveId",
                table: "ProfileObjectives");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileStatuses_Statuses_StatusId",
                table: "ProfileStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileSymptoms_Symptoms_SymptomId",
                table: "ProfileSymptoms");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileGenders_Genders_GenderId",
                table: "ProfileGenders",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "GenderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileObjectives_Objectives_ObjectiveId",
                table: "ProfileObjectives",
                column: "ObjectiveId",
                principalTable: "Objectives",
                principalColumn: "ObjectiveId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileStatuses_Statuses_StatusId",
                table: "ProfileStatuses",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileSymptoms_Symptoms_SymptomId",
                table: "ProfileSymptoms",
                column: "SymptomId",
                principalTable: "Symptoms",
                principalColumn: "SymptomId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileGenders_Genders_GenderId",
                table: "ProfileGenders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileObjectives_Objectives_ObjectiveId",
                table: "ProfileObjectives");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileStatuses_Statuses_StatusId",
                table: "ProfileStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileSymptoms_Symptoms_SymptomId",
                table: "ProfileSymptoms");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileGenders_Genders_GenderId",
                table: "ProfileGenders",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "GenderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileObjectives_Objectives_ObjectiveId",
                table: "ProfileObjectives",
                column: "ObjectiveId",
                principalTable: "Objectives",
                principalColumn: "ObjectiveId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileStatuses_Statuses_StatusId",
                table: "ProfileStatuses",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileSymptoms_Symptoms_SymptomId",
                table: "ProfileSymptoms",
                column: "SymptomId",
                principalTable: "Symptoms",
                principalColumn: "SymptomId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
