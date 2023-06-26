using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AfisLite.Broker.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEnrolmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrolment_People_PersonId",
                table: "Enrolment");

            migrationBuilder.DropForeignKey(
                name: "FK_Fingerprints_Enrolment_EnrolmentId",
                table: "Fingerprints");

            migrationBuilder.DropForeignKey(
                name: "FK_Search_People_MatchId",
                table: "Search");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Search",
                table: "Search");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrolment",
                table: "Enrolment");

            migrationBuilder.RenameTable(
                name: "Search",
                newName: "Searches");

            migrationBuilder.RenameTable(
                name: "Enrolment",
                newName: "Enrolments");

            migrationBuilder.RenameIndex(
                name: "IX_Search_MatchId",
                table: "Searches",
                newName: "IX_Searches_MatchId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrolment_PersonId",
                table: "Enrolments",
                newName: "IX_Enrolments_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Searches",
                table: "Searches",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrolments",
                table: "Enrolments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolments_People_PersonId",
                table: "Enrolments",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fingerprints_Enrolments_EnrolmentId",
                table: "Fingerprints",
                column: "EnrolmentId",
                principalTable: "Enrolments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Searches_People_MatchId",
                table: "Searches",
                column: "MatchId",
                principalTable: "People",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrolments_People_PersonId",
                table: "Enrolments");

            migrationBuilder.DropForeignKey(
                name: "FK_Fingerprints_Enrolments_EnrolmentId",
                table: "Fingerprints");

            migrationBuilder.DropForeignKey(
                name: "FK_Searches_People_MatchId",
                table: "Searches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Searches",
                table: "Searches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrolments",
                table: "Enrolments");

            migrationBuilder.RenameTable(
                name: "Searches",
                newName: "Search");

            migrationBuilder.RenameTable(
                name: "Enrolments",
                newName: "Enrolment");

            migrationBuilder.RenameIndex(
                name: "IX_Searches_MatchId",
                table: "Search",
                newName: "IX_Search_MatchId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrolments_PersonId",
                table: "Enrolment",
                newName: "IX_Enrolment_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Search",
                table: "Search",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrolment",
                table: "Enrolment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolment_People_PersonId",
                table: "Enrolment",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fingerprints_Enrolment_EnrolmentId",
                table: "Fingerprints",
                column: "EnrolmentId",
                principalTable: "Enrolment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Search_People_MatchId",
                table: "Search",
                column: "MatchId",
                principalTable: "People",
                principalColumn: "Id");
        }
    }
}
