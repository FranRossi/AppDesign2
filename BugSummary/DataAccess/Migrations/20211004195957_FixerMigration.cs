using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class FixerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FixerId",
                table: "Bugs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_FixerId",
                table: "Bugs",
                column: "FixerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_Users_FixerId",
                table: "Bugs",
                column: "FixerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_Users_FixerId",
                table: "Bugs");

            migrationBuilder.DropIndex(
                name: "IX_Bugs_FixerId",
                table: "Bugs");

            migrationBuilder.DropColumn(
                name: "FixerId",
                table: "Bugs");
        }
    }
}
