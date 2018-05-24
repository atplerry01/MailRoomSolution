using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MailRoom.Api.Migrations
{
    public partial class Addmanifest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "JobManifests",
                newName: "WayBillNumber");

            migrationBuilder.AddColumn<string>(
                name: "BranchCode",
                table: "JobManifests",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobBranchManifest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountNumber = table.Column<string>(nullable: true),
                    BranchCode = table.Column<string>(nullable: true),
                    BranchName = table.Column<string>(nullable: true),
                    CustodianName = table.Column<string>(nullable: true),
                    CustodianNumber = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    JobManifestId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Pan = table.Column<string>(nullable: true),
                    SN = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobBranchManifest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobBranchManifest_JobManifests_JobManifestId",
                        column: x => x.JobManifestId,
                        principalTable: "JobManifests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobBranchManifest_JobManifestId",
                table: "JobBranchManifest",
                column: "JobManifestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobBranchManifest");

            migrationBuilder.DropColumn(
                name: "BranchCode",
                table: "JobManifests");

            migrationBuilder.RenameColumn(
                name: "WayBillNumber",
                table: "JobManifests",
                newName: "Name");
        }
    }
}
