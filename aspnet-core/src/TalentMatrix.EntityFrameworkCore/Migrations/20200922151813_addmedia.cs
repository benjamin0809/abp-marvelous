using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TalentMatrix.Migrations
{
    public partial class addmedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Filename = table.Column<string>(maxLength: 32, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Path = table.Column<string>(maxLength: 10, nullable: false),
                    Type = table.Column<byte>(nullable: false),
                    Size = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Media");
        }
    }
}
