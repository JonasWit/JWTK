using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemyWP.Data.Migrations
{
    public partial class logsupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "PortalLogs",
                type: "character varying(70)",
                maxLength: 70,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(70)",
                oldMaxLength: 70);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PortalLogs",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "PortalLogs",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PortalLogs");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "PortalLogs");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "PortalLogs",
                type: "character varying(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(70)",
                oldMaxLength: 70,
                oldNullable: true);
        }
    }
}
