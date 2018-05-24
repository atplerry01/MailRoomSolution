using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MailRoom.Api.Migrations
{
    public partial class AddJobDataUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientBranchId",
                table: "Jobdatas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Jobdatas_ClientBranchId",
                table: "Jobdatas",
                column: "ClientBranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobdatas_ClientBranches_ClientBranchId",
                table: "Jobdatas",
                column: "ClientBranchId",
                principalTable: "ClientBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobdatas_ClientBranches_ClientBranchId",
                table: "Jobdatas");

            migrationBuilder.DropIndex(
                name: "IX_Jobdatas_ClientBranchId",
                table: "Jobdatas");

            migrationBuilder.DropColumn(
                name: "ClientBranchId",
                table: "Jobdatas");
        }
    }
}
