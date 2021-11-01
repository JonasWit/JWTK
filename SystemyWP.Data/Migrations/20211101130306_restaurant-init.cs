using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SystemyWP.Data.Migrations
{
    public partial class restaurantinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RestaurantAppMenus",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RestaurantAppDishes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    RestaurantAccessKeyId = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantAppDishes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantAppDishes_RestaurantAccessKeys_RestaurantAccessKe~",
                        column: x => x.RestaurantAccessKeyId,
                        principalTable: "RestaurantAccessKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantAppIngredients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    MeasurementUnits = table.Column<int>(type: "integer", nullable: false),
                    PricePerStack = table.Column<float>(type: "real", nullable: false),
                    StackSize = table.Column<float>(type: "real", nullable: false),
                    RestaurantAccessKeyId = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantAppIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantAppIngredients_RestaurantAccessKeys_RestaurantAcc~",
                        column: x => x.RestaurantAccessKeyId,
                        principalTable: "RestaurantAccessKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantAppDishRestaurantAppMenu",
                columns: table => new
                {
                    RestaurantAppDishesId = table.Column<long>(type: "bigint", nullable: false),
                    RestaurantAppMenusId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantAppDishRestaurantAppMenu", x => new { x.RestaurantAppDishesId, x.RestaurantAppMenusId });
                    table.ForeignKey(
                        name: "FK_RestaurantAppDishRestaurantAppMenu_RestaurantAppDishes_Rest~",
                        column: x => x.RestaurantAppDishesId,
                        principalTable: "RestaurantAppDishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantAppDishRestaurantAppMenu_RestaurantAppMenus_Resta~",
                        column: x => x.RestaurantAppMenusId,
                        principalTable: "RestaurantAppMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantAppUsedIngredients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsedAmount = table.Column<float>(type: "real", nullable: false),
                    RestaurantAppDishId = table.Column<long>(type: "bigint", nullable: false),
                    RestaurantAppIngredientId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantAppUsedIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantAppUsedIngredients_RestaurantAppDishes_Restaurant~",
                        column: x => x.RestaurantAppDishId,
                        principalTable: "RestaurantAppDishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantAppUsedIngredients_RestaurantAppIngredients_Resta~",
                        column: x => x.RestaurantAppIngredientId,
                        principalTable: "RestaurantAppIngredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantAppDishes_RestaurantAccessKeyId",
                table: "RestaurantAppDishes",
                column: "RestaurantAccessKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantAppDishRestaurantAppMenu_RestaurantAppMenusId",
                table: "RestaurantAppDishRestaurantAppMenu",
                column: "RestaurantAppMenusId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantAppIngredients_RestaurantAccessKeyId",
                table: "RestaurantAppIngredients",
                column: "RestaurantAccessKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantAppUsedIngredients_RestaurantAppDishId",
                table: "RestaurantAppUsedIngredients",
                column: "RestaurantAppDishId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantAppUsedIngredients_RestaurantAppIngredientId",
                table: "RestaurantAppUsedIngredients",
                column: "RestaurantAppIngredientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RestaurantAppDishRestaurantAppMenu");

            migrationBuilder.DropTable(
                name: "RestaurantAppUsedIngredients");

            migrationBuilder.DropTable(
                name: "RestaurantAppDishes");

            migrationBuilder.DropTable(
                name: "RestaurantAppIngredients");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RestaurantAppMenus");
        }
    }
}
