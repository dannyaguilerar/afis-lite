using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AfisLite.Broker.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddActionsAndNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fingerprints_People_PersonId",
                table: "Fingerprints");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Fingerprints",
                newName: "EnrolmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Fingerprints_PersonId",
                table: "Fingerprints",
                newName: "IX_Fingerprints_EnrolmentId");

            migrationBuilder.CreateTable(
                name: "Enrolment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UniqueId = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrolment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrolment_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Search",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MatchPersonId = table.Column<int>(type: "integer", nullable: true),
                    StartedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinishedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MatchId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Search", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Search_People_MatchId",
                        column: x => x.MatchId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Verifies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Score = table.Column<double>(type: "double precision", nullable: false),
                    CandidatePersonId = table.Column<int>(type: "integer", nullable: false),
                    StartedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinishedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CandidateId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verifies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Verifies_People_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrolment_PersonId",
                table: "Enrolment",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Search_MatchId",
                table: "Search",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Verifies_CandidateId",
                table: "Verifies",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fingerprints_Enrolment_EnrolmentId",
                table: "Fingerprints",
                column: "EnrolmentId",
                principalTable: "Enrolment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fingerprints_Enrolment_EnrolmentId",
                table: "Fingerprints");

            migrationBuilder.DropTable(
                name: "Enrolment");

            migrationBuilder.DropTable(
                name: "Search");

            migrationBuilder.DropTable(
                name: "Verifies");

            migrationBuilder.RenameColumn(
                name: "EnrolmentId",
                table: "Fingerprints",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Fingerprints_EnrolmentId",
                table: "Fingerprints",
                newName: "IX_Fingerprints_PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fingerprints_People_PersonId",
                table: "Fingerprints",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
