using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExeApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Adduser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_IdentityUserId",
                table: "Subjects",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_IdentityUserId",
                table: "Projects",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_IdentityUserId",
                table: "Projects",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_AspNetUsers_IdentityUserId",
                table: "Subjects",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_IdentityUserId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_AspNetUsers_IdentityUserId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_IdentityUserId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_IdentityUserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Projects");
        }
    }
}
