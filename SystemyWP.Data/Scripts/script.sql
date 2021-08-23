﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE TABLE "ApiLogs" (
        "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
        "LogLevel" integer NOT NULL,
        "EventName" text NULL,
        "State" text NULL,
        "Source" text NULL,
        "ExceptionMessage" text NULL,
        "StackTrace" text NULL,
        "Created" timestamp without time zone NOT NULL,
        CONSTRAINT "PK_ApiLogs" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE TABLE "LegalAppAccessKeys" (
        "Id" integer GENERATED BY DEFAULT AS IDENTITY,
        "Active" boolean NOT NULL,
        "CreatedBy" character varying(256) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        "UpdatedBy" character varying(256) NOT NULL,
        "Updated" timestamp without time zone NOT NULL,
        "Name" character varying(50) NOT NULL,
        "ExpireDate" timestamp without time zone NOT NULL,
        CONSTRAINT "PK_LegalAppAccessKeys" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE TABLE "MedicalAccessKeys" (
        "Id" integer GENERATED BY DEFAULT AS IDENTITY,
        "Active" boolean NOT NULL,
        "CreatedBy" character varying(256) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        "UpdatedBy" character varying(256) NOT NULL,
        "Updated" timestamp without time zone NOT NULL,
        "Name" character varying(50) NOT NULL,
        "ExpireDate" timestamp without time zone NOT NULL,
        CONSTRAINT "PK_MedicalAccessKeys" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE TABLE "PortalLogs" (
        "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
        "LogType" integer NOT NULL,
        "Description" character varying(500) NULL,
        "Endpoint" character varying(500) NULL,
        "ExceptionMessage" text NULL,
        "ExceptionStackTrace" text NULL,
        "UserEmail" character varying(70) NOT NULL,
        "Active" boolean NOT NULL,
        "CreatedBy" character varying(256) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        CONSTRAINT "PK_PortalLogs" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE TABLE "PortalPublications" (
        "Id" integer GENERATED BY DEFAULT AS IDENTITY,
        "Title" character varying(200) NOT NULL,
        "News" character varying(5000) NOT NULL,
        "Image" text NULL,
        "PortalPublicationCategory" integer NOT NULL,
        "Active" boolean NOT NULL,
        "CreatedBy" character varying(256) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        "UpdatedBy" character varying(256) NOT NULL,
        "Updated" timestamp without time zone NOT NULL,
        CONSTRAINT "PK_PortalPublications" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE TABLE "LegalAppBillingData" (
        "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
        "LegalAppAccessKeyId" integer NOT NULL,
        "Active" boolean NOT NULL,
        "CreatedBy" character varying(256) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        "UpdatedBy" character varying(256) NOT NULL,
        "Updated" timestamp without time zone NOT NULL,
        "Name" character varying(100) NOT NULL,
        "Street" character varying(200) NULL,
        "Address" character varying(200) NULL,
        "City" character varying(100) NULL,
        "PostalCode" character varying(50) NULL,
        "PhoneNumber" character varying(50) NULL,
        "FaxNumber" character varying(50) NULL,
        "Nip" character varying(50) NULL,
        "Regon" character varying(50) NULL,
        CONSTRAINT "PK_LegalAppBillingData" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_LegalAppBillingData_LegalAppAccessKeys_LegalAppAccessKeyId" FOREIGN KEY ("LegalAppAccessKeyId") REFERENCES "LegalAppAccessKeys" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE TABLE "LegalAppClients" (
        "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
        "Name" character varying(50) NOT NULL,
        "LegalAppAccessKeyId" integer NOT NULL,
        "Active" boolean NOT NULL,
        "CreatedBy" character varying(256) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        "UpdatedBy" character varying(256) NOT NULL,
        "Updated" timestamp without time zone NOT NULL,
        CONSTRAINT "PK_LegalAppClients" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_LegalAppClients_LegalAppAccessKeys_LegalAppAccessKeyId" FOREIGN KEY ("LegalAppAccessKeyId") REFERENCES "LegalAppAccessKeys" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE TABLE "LegalAppReminders" (
        "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
        "Name" character varying(100) NOT NULL,
        "Public" boolean NOT NULL,
        "AuthorId" character varying(256) NOT NULL,
        "LegalAppAccessKeyId" integer NOT NULL,
        "Active" boolean NOT NULL,
        "CreatedBy" character varying(256) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        "UpdatedBy" character varying(256) NOT NULL,
        "Updated" timestamp without time zone NOT NULL,
        "AllDayEvent" boolean NOT NULL,
        "ReminderCategory" integer NOT NULL,
        "Message" character varying(200) NOT NULL,
        "Start" timestamp without time zone NOT NULL,
        "End" timestamp without time zone NOT NULL,
        CONSTRAINT "PK_LegalAppReminders" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_LegalAppReminders_LegalAppAccessKeys_LegalAppAccessKeyId" FOREIGN KEY ("LegalAppAccessKeyId") REFERENCES "LegalAppAccessKeys" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE TABLE "MedicalAppPatients" (
        "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
        "MedicalAccessKeyId" integer NOT NULL,
        "Active" boolean NOT NULL,
        "CreatedBy" character varying(256) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        "UpdatedBy" character varying(256) NOT NULL,
        "Updated" timestamp without time zone NOT NULL,
        "Name" text NULL,
        "Surname" text NULL,
        CONSTRAINT "PK_MedicalAppPatients" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_MedicalAppPatients_MedicalAccessKeys_MedicalAccessKeyId" FOREIGN KEY ("MedicalAccessKeyId") REFERENCES "MedicalAccessKeys" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE TABLE "Users" (
        "Id" text NOT NULL,
        "Image" text NULL,
        "Email" character varying(265) NOT NULL,
        "Username" character varying(265) NOT NULL,
        "LegalAppAccessKeyId" integer NULL,
        "MedicalAccessKeyId" integer NULL,
        "PhoneNumber" text NULL,
        "CompanyFullName" text NULL,
        "Name" text NULL,
        "Surname" text NULL,
        "Address" text NULL,
        "AddressCorrespondence" text NULL,
        "City" text NULL,
        "Vivodership" text NULL,
        "Country" text NULL,
        "PostCode" text NULL,
        "Nip" text NULL,
        "Regon" text NULL,
        "Krs" text NULL,
        "LastLogin" timestamp without time zone NULL,
        "Active" boolean NOT NULL,
        "CreatedBy" character varying(256) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        CONSTRAINT "PK_Users" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Users_LegalAppAccessKeys_LegalAppAccessKeyId" FOREIGN KEY ("LegalAppAccessKeyId") REFERENCES "LegalAppAccessKeys" ("Id") ON DELETE SET NULL,
        CONSTRAINT "FK_Users_MedicalAccessKeys_MedicalAccessKeyId" FOREIGN KEY ("MedicalAccessKeyId") REFERENCES "MedicalAccessKeys" ("Id") ON DELETE SET NULL
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE TABLE "LegalAppCases" (
        "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
        "Name" character varying(50) NOT NULL,
        "Signature" character varying(200) NULL,
        "Description" character varying(1000) NULL,
        "Group" character varying(200) NOT NULL,
        "LegalAppClientId" bigint NOT NULL,
        "Active" boolean NOT NULL,
        "CreatedBy" character varying(256) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        "UpdatedBy" character varying(256) NOT NULL,
        "Updated" timestamp without time zone NOT NULL,
        CONSTRAINT "PK_LegalAppCases" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_LegalAppCases_LegalAppClients_LegalAppClientId" FOREIGN KEY ("LegalAppClientId") REFERENCES "LegalAppClients" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE TABLE "LegalAppClientNotes" (
        "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
        "Public" boolean NOT NULL,
        "AuthorId" character varying(256) NOT NULL,
        "LegalAppClientId" bigint NOT NULL,
        "Active" boolean NOT NULL,
        "CreatedBy" character varying(256) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        "UpdatedBy" character varying(256) NOT NULL,
        "Updated" timestamp without time zone NOT NULL,
        "Title" character varying(100) NOT NULL,
        "Message" character varying(1000) NULL,
        CONSTRAINT "PK_LegalAppClientNotes" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_LegalAppClientNotes_LegalAppClients_LegalAppClientId" FOREIGN KEY ("LegalAppClientId") REFERENCES "LegalAppClients" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE TABLE "LegalAppClientWorkRecords" (
        "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
        "UserId" text NOT NULL,
        "LawyerName" character varying(200) NULL,
        "Name" character varying(100) NOT NULL,
        "Description" character varying(300) NULL,
        "EventDate" timestamp without time zone NOT NULL,
        "Hours" integer NOT NULL,
        "Minutes" integer NOT NULL,
        "Vat" integer NOT NULL,
        "Rate" numeric NOT NULL,
        "Amount" numeric NOT NULL,
        "LegalAppClientId" bigint NOT NULL,
        "Active" boolean NOT NULL,
        "CreatedBy" character varying(256) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        CONSTRAINT "PK_LegalAppClientWorkRecords" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_LegalAppClientWorkRecords_LegalAppClients_LegalAppClientId" FOREIGN KEY ("LegalAppClientId") REFERENCES "LegalAppClients" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE TABLE "LegalAppContacts" (
        "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
        "Comment" character varying(300) NULL,
        "Title" character varying(200) NOT NULL,
        "Name" character varying(200) NULL,
        "Surname" character varying(200) NULL,
        "LegalAppClientId" bigint NOT NULL,
        "Active" boolean NOT NULL,
        "CreatedBy" character varying(256) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        CONSTRAINT "PK_LegalAppContacts" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_LegalAppContacts_LegalAppClients_LegalAppClientId" FOREIGN KEY ("LegalAppClientId") REFERENCES "LegalAppClients" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE TABLE "LegalAppDataAccesses" (
        "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
        "LegalAppRestrictedType" integer NOT NULL,
        "ItemId" bigint NOT NULL,
        "UserId" text NULL,
        "LegalAppAccessKeyId" integer NOT NULL,
        "Active" boolean NOT NULL,
        "CreatedBy" character varying(256) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        CONSTRAINT "PK_LegalAppDataAccesses" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_LegalAppDataAccesses_LegalAppAccessKeys_LegalAppAccessKeyId" FOREIGN KEY ("LegalAppAccessKeyId") REFERENCES "LegalAppAccessKeys" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_LegalAppDataAccesses_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE TABLE "MedicalAppDataAccesses" (
        "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
        "MedicalAppRestrictedType" integer NOT NULL,
        "ItemId" bigint NOT NULL,
        "UserId" text NULL,
        "MedicalAccessKeyId" integer NOT NULL,
        "Active" boolean NOT NULL,
        "CreatedBy" character varying(256) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        CONSTRAINT "PK_MedicalAppDataAccesses" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_MedicalAppDataAccesses_MedicalAccessKeys_MedicalAccessKeyId" FOREIGN KEY ("MedicalAccessKeyId") REFERENCES "MedicalAccessKeys" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_MedicalAppDataAccesses_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE TABLE "LegalAppCaseDeadlines" (
        "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
        "LegalAppCaseId" bigint NOT NULL,
        "Active" boolean NOT NULL,
        "CreatedBy" character varying(256) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        "Message" character varying(200) NOT NULL,
        "Deadline" timestamp without time zone NOT NULL,
        CONSTRAINT "PK_LegalAppCaseDeadlines" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_LegalAppCaseDeadlines_LegalAppCases_LegalAppCaseId" FOREIGN KEY ("LegalAppCaseId") REFERENCES "LegalAppCases" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE TABLE "LegalAppCaseNotes" (
        "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
        "LegalAppCaseId" bigint NOT NULL,
        "Active" boolean NOT NULL,
        "CreatedBy" character varying(256) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        "UpdatedBy" character varying(256) NOT NULL,
        "Updated" timestamp without time zone NOT NULL,
        "Title" character varying(100) NOT NULL,
        "Message" character varying(1000) NULL,
        CONSTRAINT "PK_LegalAppCaseNotes" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_LegalAppCaseNotes_LegalAppCases_LegalAppCaseId" FOREIGN KEY ("LegalAppCaseId") REFERENCES "LegalAppCases" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE TABLE "LegalAppEmailAddresses" (
        "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
        "Comment" character varying(100) NULL,
        "Email" character varying(256) NOT NULL,
        "LegalAppContactDetailId" bigint NULL,
        "Active" boolean NOT NULL,
        "CreatedBy" character varying(256) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        CONSTRAINT "PK_LegalAppEmailAddresses" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_LegalAppEmailAddresses_LegalAppContacts_LegalAppContactDeta~" FOREIGN KEY ("LegalAppContactDetailId") REFERENCES "LegalAppContacts" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE TABLE "LegalAppPhoneNumbers" (
        "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
        "Comment" character varying(100) NULL,
        "Number" character varying(100) NOT NULL,
        "LegalAppContactDetailId" bigint NULL,
        "Active" boolean NOT NULL,
        "CreatedBy" character varying(256) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        CONSTRAINT "PK_LegalAppPhoneNumbers" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_LegalAppPhoneNumbers_LegalAppContacts_LegalAppContactDetail~" FOREIGN KEY ("LegalAppContactDetailId") REFERENCES "LegalAppContacts" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE TABLE "LegalAppPhysicalAddresses" (
        "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
        "Comment" character varying(100) NULL,
        "Country" character varying(75) NULL,
        "City" character varying(75) NULL,
        "Street" character varying(200) NOT NULL,
        "Building" character varying(50) NULL,
        "PostCode" character varying(50) NULL,
        "LegalAppContactDetailId" bigint NULL,
        "Active" boolean NOT NULL,
        "CreatedBy" character varying(256) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        CONSTRAINT "PK_LegalAppPhysicalAddresses" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_LegalAppPhysicalAddresses_LegalAppContacts_LegalAppContactD~" FOREIGN KEY ("LegalAppContactDetailId") REFERENCES "LegalAppContacts" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE INDEX "IX_LegalAppBillingData_LegalAppAccessKeyId" ON "LegalAppBillingData" ("LegalAppAccessKeyId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE INDEX "IX_LegalAppCaseDeadlines_LegalAppCaseId" ON "LegalAppCaseDeadlines" ("LegalAppCaseId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE INDEX "IX_LegalAppCaseNotes_LegalAppCaseId" ON "LegalAppCaseNotes" ("LegalAppCaseId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE INDEX "IX_LegalAppCases_LegalAppClientId" ON "LegalAppCases" ("LegalAppClientId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE INDEX "IX_LegalAppClientNotes_LegalAppClientId" ON "LegalAppClientNotes" ("LegalAppClientId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE INDEX "IX_LegalAppClients_LegalAppAccessKeyId" ON "LegalAppClients" ("LegalAppAccessKeyId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE INDEX "IX_LegalAppClientWorkRecords_LegalAppClientId" ON "LegalAppClientWorkRecords" ("LegalAppClientId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE INDEX "IX_LegalAppContacts_LegalAppClientId" ON "LegalAppContacts" ("LegalAppClientId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE INDEX "IX_LegalAppDataAccesses_ItemId" ON "LegalAppDataAccesses" ("ItemId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE INDEX "IX_LegalAppDataAccesses_LegalAppAccessKeyId" ON "LegalAppDataAccesses" ("LegalAppAccessKeyId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE INDEX "IX_LegalAppDataAccesses_UserId" ON "LegalAppDataAccesses" ("UserId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE INDEX "IX_LegalAppEmailAddresses_LegalAppContactDetailId" ON "LegalAppEmailAddresses" ("LegalAppContactDetailId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE INDEX "IX_LegalAppPhoneNumbers_LegalAppContactDetailId" ON "LegalAppPhoneNumbers" ("LegalAppContactDetailId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE INDEX "IX_LegalAppPhysicalAddresses_LegalAppContactDetailId" ON "LegalAppPhysicalAddresses" ("LegalAppContactDetailId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE INDEX "IX_LegalAppReminders_LegalAppAccessKeyId" ON "LegalAppReminders" ("LegalAppAccessKeyId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE INDEX "IX_MedicalAppDataAccesses_ItemId" ON "MedicalAppDataAccesses" ("ItemId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE INDEX "IX_MedicalAppDataAccesses_MedicalAccessKeyId" ON "MedicalAppDataAccesses" ("MedicalAccessKeyId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE INDEX "IX_MedicalAppDataAccesses_UserId" ON "MedicalAppDataAccesses" ("UserId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE INDEX "IX_MedicalAppPatients_MedicalAccessKeyId" ON "MedicalAppPatients" ("MedicalAccessKeyId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE INDEX "IX_Users_LegalAppAccessKeyId" ON "Users" ("LegalAppAccessKeyId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    CREATE INDEX "IX_Users_MedicalAccessKeyId" ON "Users" ("MedicalAccessKeyId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210820091257_init') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20210820091257_init', '5.0.9');
    END IF;
END $$;
COMMIT;
