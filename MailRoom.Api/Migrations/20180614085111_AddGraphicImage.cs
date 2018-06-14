using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MailRoom.Api.Migrations
{
    public partial class AddGraphicImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GraphicImage",
                table: "JobManifests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GraphicImage",
                table: "JobManifestBranches",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GraphicImage",
                table: "JobManifests");

            migrationBuilder.DropColumn(
                name: "GraphicImage",
                table: "JobManifestBranches");
        }
    }
}
