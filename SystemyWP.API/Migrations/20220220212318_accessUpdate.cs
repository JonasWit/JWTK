using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemyWP.API.Migrations
{
    public partial class accessUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AccessKeys_AccessKeyId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AccessKeyId",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "AccessKeyId",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AccessKeys",
                type: "character varying(256)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccessKeys_UserId",
                table: "AccessKeys",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessKeys_Users_UserId",
                table: "AccessKeys",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessKeys_Users_UserId",
                table: "AccessKeys");

            migrationBuilder.DropIndex(
                name: "IX_AccessKeys_UserId",
                table: "AccessKeys");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AccessKeys");

            migrationBuilder.AlterColumn<string>(
                name: "AccessKeyId",
                table: "Users",
                type: "character varying(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AccessKeyId",
                table: "Users",
                column: "AccessKeyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AccessKeys_AccessKeyId",
                table: "Users",
                column: "AccessKeyId",
                principalTable: "AccessKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
