using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinhaSaudeFeminina.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeProfilerelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileGenders_Profiles_ProfileId",
                table: "ProfileGenders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileObjectives_Profiles_ProfileId",
                table: "ProfileObjectives");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileStatuses_Profiles_ProfileId",
                table: "ProfileStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileSymptoms_Profiles_ProfileId",
                table: "ProfileSymptoms");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileGenders_Profiles_ProfileId",
                table: "ProfileGenders",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileObjectives_Profiles_ProfileId",
                table: "ProfileObjectives",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileStatuses_Profiles_ProfileId",
                table: "ProfileStatuses",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileSymptoms_Profiles_ProfileId",
                table: "ProfileSymptoms",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileGenders_Profiles_ProfileId",
                table: "ProfileGenders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileObjectives_Profiles_ProfileId",
                table: "ProfileObjectives");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileStatuses_Profiles_ProfileId",
                table: "ProfileStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileSymptoms_Profiles_ProfileId",
                table: "ProfileSymptoms");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileGenders_Profiles_ProfileId",
                table: "ProfileGenders",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileObjectives_Profiles_ProfileId",
                table: "ProfileObjectives",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileStatuses_Profiles_ProfileId",
                table: "ProfileStatuses",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileSymptoms_Profiles_ProfileId",
                table: "ProfileSymptoms",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
