using Microsoft.EntityFrameworkCore.Migrations;
using MinimalAPI.Models;
using System.Collections.Generic;

#nullable disable

namespace MinimalAPI.Migrations
{
    public partial class FixedShit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InterestUser",
                columns: table => new
                {
                    InterestsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestUser", x => new { x.InterestsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_InterestUser_Interests_InterestsId",
                        column: x => x.InterestsId,
                        principalTable: "Interests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterestUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.Sql(
                @"INSERT INTO dbo.InterestUser (InterestsId, UsersId)
                SELECT Id, UserId FROM dbo.Interests
                WHERE UserId IS NOT NULL"
                );

            migrationBuilder.DropForeignKey(
                name: "FK_Interests_Users_UserId",
                table: "Interests");

            migrationBuilder.DropIndex(
                name: "IX_Interests_UserId",
                table: "Interests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Interests");

            

            migrationBuilder.CreateIndex(
                name: "IX_InterestUser_UsersId",
                table: "InterestUser",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterestUser");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Interests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interests_UserId",
                table: "Interests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Interests_Users_UserId",
                table: "Interests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
