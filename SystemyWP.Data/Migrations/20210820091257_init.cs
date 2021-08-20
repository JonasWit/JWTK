using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SystemyWP.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LogLevel = table.Column<int>(type: "integer", nullable: false),
                    EventName = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    Source = table.Column<string>(type: "text", nullable: true),
                    ExceptionMessage = table.Column<string>(type: "text", nullable: true),
                    StackTrace = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LegalAppAccessKeys",
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
                    table.PrimaryKey("PK_LegalAppAccessKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalAccessKeys",
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
                    table.PrimaryKey("PK_MedicalAccessKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PortalLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LogType = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Endpoint = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ExceptionMessage = table.Column<string>(type: "text", nullable: true),
                    ExceptionStackTrace = table.Column<string>(type: "text", nullable: true),
                    UserEmail = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortalLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PortalPublications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    News = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true),
                    PortalPublicationCategory = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortalPublications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LegalAppBillingData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LegalAppAccessKeyId = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    FaxNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Nip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Regon = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalAppBillingData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalAppBillingData_LegalAppAccessKeys_LegalAppAccessKeyId",
                        column: x => x.LegalAppAccessKeyId,
                        principalTable: "LegalAppAccessKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalAppClients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LegalAppAccessKeyId = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalAppClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalAppClients_LegalAppAccessKeys_LegalAppAccessKeyId",
                        column: x => x.LegalAppAccessKeyId,
                        principalTable: "LegalAppAccessKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalAppReminders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Public = table.Column<bool>(type: "boolean", nullable: false),
                    AuthorId = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    LegalAppAccessKeyId = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AllDayEvent = table.Column<bool>(type: "boolean", nullable: false),
                    ReminderCategory = table.Column<int>(type: "integer", nullable: false),
                    Message = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalAppReminders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalAppReminders_LegalAppAccessKeys_LegalAppAccessKeyId",
                        column: x => x.LegalAppAccessKeyId,
                        principalTable: "LegalAppAccessKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalAppPatients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MedicalAccessKeyId = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalAppPatients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalAppPatients_MedicalAccessKeys_MedicalAccessKeyId",
                        column: x => x.MedicalAccessKeyId,
                        principalTable: "MedicalAccessKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "character varying(265)", maxLength: 265, nullable: false),
                    Username = table.Column<string>(type: "character varying(265)", maxLength: 265, nullable: false),
                    LegalAppAccessKeyId = table.Column<int>(type: "integer", nullable: true),
                    MedicalAccessKeyId = table.Column<int>(type: "integer", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    CompanyFullName = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    AddressCorrespondence = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Vivodership = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    PostCode = table.Column<string>(type: "text", nullable: true),
                    Nip = table.Column<string>(type: "text", nullable: true),
                    Regon = table.Column<string>(type: "text", nullable: true),
                    Krs = table.Column<string>(type: "text", nullable: true),
                    LastLogin = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_LegalAppAccessKeys_LegalAppAccessKeyId",
                        column: x => x.LegalAppAccessKeyId,
                        principalTable: "LegalAppAccessKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Users_MedicalAccessKeys_MedicalAccessKeyId",
                        column: x => x.MedicalAccessKeyId,
                        principalTable: "MedicalAccessKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "LegalAppCases",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Signature = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Group = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    LegalAppClientId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalAppCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalAppCases_LegalAppClients_LegalAppClientId",
                        column: x => x.LegalAppClientId,
                        principalTable: "LegalAppClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalAppClientNotes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Public = table.Column<bool>(type: "boolean", nullable: false),
                    AuthorId = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    LegalAppClientId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalAppClientNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalAppClientNotes_LegalAppClients_LegalAppClientId",
                        column: x => x.LegalAppClientId,
                        principalTable: "LegalAppClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalAppClientWorkRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LawyerName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    EventDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Hours = table.Column<int>(type: "integer", nullable: false),
                    Minutes = table.Column<int>(type: "integer", nullable: false),
                    Vat = table.Column<int>(type: "integer", nullable: false),
                    Rate = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    LegalAppClientId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalAppClientWorkRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalAppClientWorkRecords_LegalAppClients_LegalAppClientId",
                        column: x => x.LegalAppClientId,
                        principalTable: "LegalAppClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalAppContacts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Comment = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Surname = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    LegalAppClientId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalAppContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalAppContacts_LegalAppClients_LegalAppClientId",
                        column: x => x.LegalAppClientId,
                        principalTable: "LegalAppClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalAppDataAccesses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LegalAppRestrictedType = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    LegalAppAccessKeyId = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalAppDataAccesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalAppDataAccesses_LegalAppAccessKeys_LegalAppAccessKeyId",
                        column: x => x.LegalAppAccessKeyId,
                        principalTable: "LegalAppAccessKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LegalAppDataAccesses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalAppDataAccesses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MedicalAppRestrictedType = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    MedicalAccessKeyId = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalAppDataAccesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalAppDataAccesses_MedicalAccessKeys_MedicalAccessKeyId",
                        column: x => x.MedicalAccessKeyId,
                        principalTable: "MedicalAccessKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalAppDataAccesses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalAppCaseDeadlines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LegalAppCaseId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Message = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Deadline = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalAppCaseDeadlines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalAppCaseDeadlines_LegalAppCases_LegalAppCaseId",
                        column: x => x.LegalAppCaseId,
                        principalTable: "LegalAppCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalAppCaseNotes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LegalAppCaseId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalAppCaseNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalAppCaseNotes_LegalAppCases_LegalAppCaseId",
                        column: x => x.LegalAppCaseId,
                        principalTable: "LegalAppCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalAppEmailAddresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Comment = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    LegalAppContactDetailId = table.Column<long>(type: "bigint", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalAppEmailAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalAppEmailAddresses_LegalAppContacts_LegalAppContactDeta~",
                        column: x => x.LegalAppContactDetailId,
                        principalTable: "LegalAppContacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalAppPhoneNumbers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Comment = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LegalAppContactDetailId = table.Column<long>(type: "bigint", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalAppPhoneNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalAppPhoneNumbers_LegalAppContacts_LegalAppContactDetail~",
                        column: x => x.LegalAppContactDetailId,
                        principalTable: "LegalAppContacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalAppPhysicalAddresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Comment = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    City = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    Street = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Building = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PostCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    LegalAppContactDetailId = table.Column<long>(type: "bigint", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalAppPhysicalAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalAppPhysicalAddresses_LegalAppContacts_LegalAppContactD~",
                        column: x => x.LegalAppContactDetailId,
                        principalTable: "LegalAppContacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppBillingData_LegalAppAccessKeyId",
                table: "LegalAppBillingData",
                column: "LegalAppAccessKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppCaseDeadlines_LegalAppCaseId",
                table: "LegalAppCaseDeadlines",
                column: "LegalAppCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppCaseNotes_LegalAppCaseId",
                table: "LegalAppCaseNotes",
                column: "LegalAppCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppCases_LegalAppClientId",
                table: "LegalAppCases",
                column: "LegalAppClientId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppClientNotes_LegalAppClientId",
                table: "LegalAppClientNotes",
                column: "LegalAppClientId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppClients_LegalAppAccessKeyId",
                table: "LegalAppClients",
                column: "LegalAppAccessKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppClientWorkRecords_LegalAppClientId",
                table: "LegalAppClientWorkRecords",
                column: "LegalAppClientId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppContacts_LegalAppClientId",
                table: "LegalAppContacts",
                column: "LegalAppClientId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppDataAccesses_ItemId",
                table: "LegalAppDataAccesses",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppDataAccesses_LegalAppAccessKeyId",
                table: "LegalAppDataAccesses",
                column: "LegalAppAccessKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppDataAccesses_UserId",
                table: "LegalAppDataAccesses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppEmailAddresses_LegalAppContactDetailId",
                table: "LegalAppEmailAddresses",
                column: "LegalAppContactDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppPhoneNumbers_LegalAppContactDetailId",
                table: "LegalAppPhoneNumbers",
                column: "LegalAppContactDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppPhysicalAddresses_LegalAppContactDetailId",
                table: "LegalAppPhysicalAddresses",
                column: "LegalAppContactDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalAppReminders_LegalAppAccessKeyId",
                table: "LegalAppReminders",
                column: "LegalAppAccessKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAppDataAccesses_ItemId",
                table: "MedicalAppDataAccesses",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAppDataAccesses_MedicalAccessKeyId",
                table: "MedicalAppDataAccesses",
                column: "MedicalAccessKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAppDataAccesses_UserId",
                table: "MedicalAppDataAccesses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAppPatients_MedicalAccessKeyId",
                table: "MedicalAppPatients",
                column: "MedicalAccessKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LegalAppAccessKeyId",
                table: "Users",
                column: "LegalAppAccessKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_MedicalAccessKeyId",
                table: "Users",
                column: "MedicalAccessKeyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiLogs");

            migrationBuilder.DropTable(
                name: "LegalAppBillingData");

            migrationBuilder.DropTable(
                name: "LegalAppCaseDeadlines");

            migrationBuilder.DropTable(
                name: "LegalAppCaseNotes");

            migrationBuilder.DropTable(
                name: "LegalAppClientNotes");

            migrationBuilder.DropTable(
                name: "LegalAppClientWorkRecords");

            migrationBuilder.DropTable(
                name: "LegalAppDataAccesses");

            migrationBuilder.DropTable(
                name: "LegalAppEmailAddresses");

            migrationBuilder.DropTable(
                name: "LegalAppPhoneNumbers");

            migrationBuilder.DropTable(
                name: "LegalAppPhysicalAddresses");

            migrationBuilder.DropTable(
                name: "LegalAppReminders");

            migrationBuilder.DropTable(
                name: "MedicalAppDataAccesses");

            migrationBuilder.DropTable(
                name: "MedicalAppPatients");

            migrationBuilder.DropTable(
                name: "PortalLogs");

            migrationBuilder.DropTable(
                name: "PortalPublications");

            migrationBuilder.DropTable(
                name: "LegalAppCases");

            migrationBuilder.DropTable(
                name: "LegalAppContacts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "LegalAppClients");

            migrationBuilder.DropTable(
                name: "MedicalAccessKeys");

            migrationBuilder.DropTable(
                name: "LegalAppAccessKeys");
        }
    }
}
