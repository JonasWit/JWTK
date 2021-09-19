using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SystemyWP.Data.Migrations
{
    public partial class restaurantappinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LegalAppBillingData_LegalAppAccessKeys_LegalAppAccessKeyId",
                table: "LegalAppBillingData");

            migrationBuilder.DropForeignKey(
                name: "FK_LegalAppClients_LegalAppAccessKeys_LegalAppAccessKeyId",
                table: "LegalAppClients");

            migrationBuilder.DropForeignKey(
                name: "FK_LegalAppDataAccesses_LegalAppAccessKeys_LegalAppAccessKeyId",
                table: "LegalAppDataAccesses");

            migrationBuilder.DropForeignKey(
                name: "FK_LegalAppReminders_LegalAppAccessKeys_LegalAppAccessKeyId",
                table: "LegalAppReminders");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_LegalAppAccessKeys_LegalAppAccessKeyId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_LegalAppReminders_LegalAppAccessKeyId",
                table: "LegalAppReminders");

            migrationBuilder.DropIndex(
                name: "IX_LegalAppDataAccesses_LegalAppAccessKeyId",
                table: "LegalAppDataAccesses");

            migrationBuilder.DropIndex(
                name: "IX_LegalAppClients_LegalAppAccessKeyId",
                table: "LegalAppClients");

            migrationBuilder.DropIndex(
                name: "IX_LegalAppBillingData_LegalAppAccessKeyId",
                table: "LegalAppBillingData");

            migrationBuilder.RenameColumn(
                name: "LegalAppAccessKeyId",
                table: "Users",
                newName: "RestaurantAccessKeyId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_LegalAppAccessKeyId",
                table: "Users",
                newName: "IX_Users_RestaurantAccessKeyId");

            migrationBuilder.AddColumn<int>(
                name: "LegalAccessKeyId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LegalAccessKeyId",
                table: "LegalAppReminders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LegalAccessKeyId",
                table: "LegalAppDataAccesses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LegalAccessKeyId",
                table: "LegalAppClients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LegalAccessKeyId",
                table: "LegalAppBillingData",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RestaurantAccessKey",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantAccessKey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantAppDataAccess",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RestaurantAppRestrictedType = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    RestaurantAccessKeyId = table.Column<int>(type: "integer", nullable: true),
                    RestaurantAppAccessKeyId = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantAppDataAccess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantAppDataAccess_RestaurantAccessKey_RestaurantAcces~",
                        column: x => x.RestaurantAccessKeyId,
                        principalTable: "RestaurantAccessKey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantAppDataAccess_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantAppMenu",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RestaurantAccessKeyId = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantAppMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantAppMenu_RestaurantAccessKey_RestaurantAccessKeyId",
                        column: x => x.RestaurantAccessKeyId,
                        principalTable: "RestaurantAccessKey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_LegalAccessKeyId",
                table: "Users",
                column: "LegalAccessKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppReminders_LegalAccessKeyId",
                table: "LegalAppReminders",
                column: "LegalAccessKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppDataAccesses_LegalAccessKeyId",
                table: "LegalAppDataAccesses",
                column: "LegalAccessKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppClients_LegalAccessKeyId",
                table: "LegalAppClients",
                column: "LegalAccessKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppBillingData_LegalAccessKeyId",
                table: "LegalAppBillingData",
                column: "LegalAccessKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantAppDataAccess_ItemId",
                table: "RestaurantAppDataAccess",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantAppDataAccess_RestaurantAccessKeyId",
                table: "RestaurantAppDataAccess",
                column: "RestaurantAccessKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantAppDataAccess_UserId",
                table: "RestaurantAppDataAccess",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantAppMenu_RestaurantAccessKeyId",
                table: "RestaurantAppMenu",
                column: "RestaurantAccessKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_LegalAppBillingData_LegalAppAccessKeys_LegalAccessKeyId",
                table: "LegalAppBillingData",
                column: "LegalAccessKeyId",
                principalTable: "LegalAppAccessKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LegalAppClients_LegalAppAccessKeys_LegalAccessKeyId",
                table: "LegalAppClients",
                column: "LegalAccessKeyId",
                principalTable: "LegalAppAccessKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LegalAppDataAccesses_LegalAppAccessKeys_LegalAccessKeyId",
                table: "LegalAppDataAccesses",
                column: "LegalAccessKeyId",
                principalTable: "LegalAppAccessKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LegalAppReminders_LegalAppAccessKeys_LegalAccessKeyId",
                table: "LegalAppReminders",
                column: "LegalAccessKeyId",
                principalTable: "LegalAppAccessKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_LegalAppAccessKeys_LegalAccessKeyId",
                table: "Users",
                column: "LegalAccessKeyId",
                principalTable: "LegalAppAccessKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_RestaurantAccessKey_RestaurantAccessKeyId",
                table: "Users",
                column: "RestaurantAccessKeyId",
                principalTable: "RestaurantAccessKey",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LegalAppBillingData_LegalAppAccessKeys_LegalAccessKeyId",
                table: "LegalAppBillingData");

            migrationBuilder.DropForeignKey(
                name: "FK_LegalAppClients_LegalAppAccessKeys_LegalAccessKeyId",
                table: "LegalAppClients");

            migrationBuilder.DropForeignKey(
                name: "FK_LegalAppDataAccesses_LegalAppAccessKeys_LegalAccessKeyId",
                table: "LegalAppDataAccesses");

            migrationBuilder.DropForeignKey(
                name: "FK_LegalAppReminders_LegalAppAccessKeys_LegalAccessKeyId",
                table: "LegalAppReminders");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_LegalAppAccessKeys_LegalAccessKeyId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_RestaurantAccessKey_RestaurantAccessKeyId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "RestaurantAppDataAccess");

            migrationBuilder.DropTable(
                name: "RestaurantAppMenu");

            migrationBuilder.DropTable(
                name: "RestaurantAccessKey");

            migrationBuilder.DropIndex(
                name: "IX_Users_LegalAccessKeyId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_LegalAppReminders_LegalAccessKeyId",
                table: "LegalAppReminders");

            migrationBuilder.DropIndex(
                name: "IX_LegalAppDataAccesses_LegalAccessKeyId",
                table: "LegalAppDataAccesses");

            migrationBuilder.DropIndex(
                name: "IX_LegalAppClients_LegalAccessKeyId",
                table: "LegalAppClients");

            migrationBuilder.DropIndex(
                name: "IX_LegalAppBillingData_LegalAccessKeyId",
                table: "LegalAppBillingData");

            migrationBuilder.DropColumn(
                name: "LegalAccessKeyId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LegalAccessKeyId",
                table: "LegalAppReminders");

            migrationBuilder.DropColumn(
                name: "LegalAccessKeyId",
                table: "LegalAppDataAccesses");

            migrationBuilder.DropColumn(
                name: "LegalAccessKeyId",
                table: "LegalAppClients");

            migrationBuilder.DropColumn(
                name: "LegalAccessKeyId",
                table: "LegalAppBillingData");

            migrationBuilder.RenameColumn(
                name: "RestaurantAccessKeyId",
                table: "Users",
                newName: "LegalAppAccessKeyId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RestaurantAccessKeyId",
                table: "Users",
                newName: "IX_Users_LegalAppAccessKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppReminders_LegalAppAccessKeyId",
                table: "LegalAppReminders",
                column: "LegalAppAccessKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppDataAccesses_LegalAppAccessKeyId",
                table: "LegalAppDataAccesses",
                column: "LegalAppAccessKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppClients_LegalAppAccessKeyId",
                table: "LegalAppClients",
                column: "LegalAppAccessKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppBillingData_LegalAppAccessKeyId",
                table: "LegalAppBillingData",
                column: "LegalAppAccessKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_LegalAppBillingData_LegalAppAccessKeys_LegalAppAccessKeyId",
                table: "LegalAppBillingData",
                column: "LegalAppAccessKeyId",
                principalTable: "LegalAppAccessKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LegalAppClients_LegalAppAccessKeys_LegalAppAccessKeyId",
                table: "LegalAppClients",
                column: "LegalAppAccessKeyId",
                principalTable: "LegalAppAccessKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LegalAppDataAccesses_LegalAppAccessKeys_LegalAppAccessKeyId",
                table: "LegalAppDataAccesses",
                column: "LegalAppAccessKeyId",
                principalTable: "LegalAppAccessKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LegalAppReminders_LegalAppAccessKeys_LegalAppAccessKeyId",
                table: "LegalAppReminders",
                column: "LegalAppAccessKeyId",
                principalTable: "LegalAppAccessKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_LegalAppAccessKeys_LegalAppAccessKeyId",
                table: "Users",
                column: "LegalAppAccessKeyId",
                principalTable: "LegalAppAccessKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
