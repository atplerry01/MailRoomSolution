using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MailRoom.Api.Migrations
{
    public partial class InitialUpdateJobBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "ClientBranches",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPhone",
                table: "ClientBranches",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ClientBranches",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "ClientBranches");

            migrationBuilder.DropColumn(
                name: "ContactPhone",
                table: "ClientBranches");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ClientBranches");
        }
    }
}
