using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MailRoom.Api.Migrations
{
    public partial class UpdateManifest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobBranchManifest");

            migrationBuilder.RenameColumn(
                name: "BranchCode",
                table: "JobManifests",
                newName: "JobId");

            migrationBuilder.CreateTable(
                name: "JobManifestBranch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JobManifestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobManifestBranch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobManifestBranch_JobManifests_JobManifestId",
                        column: x => x.JobManifestId,
                        principalTable: "JobManifests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobManifestLog",
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
                    JobManifestBranchId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Pan = table.Column<string>(nullable: true),
                    SN = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobManifestLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobManifestLog_JobManifestBranch_JobManifestBranchId",
                        column: x => x.JobManifestBranchId,
                        principalTable: "JobManifestBranch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobManifestBranch_JobManifestId",
                table: "JobManifestBranch",
                column: "JobManifestId");

            migrationBuilder.CreateIndex(
                name: "IX_JobManifestLog_JobManifestBranchId",
                table: "JobManifestLog",
                column: "JobManifestBranchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobManifestLog");

            migrationBuilder.DropTable(
                name: "JobManifestBranch");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "JobManifests",
                newName: "BranchCode");

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
    }
}
