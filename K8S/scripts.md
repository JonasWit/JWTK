﻿### migrations - default
dotnet ef migrations add <name>

### migrations - update ef tool
dotnet tool update --global dotnet-ef

### migrations - use from Data path

dotnet ef migrations add <name> -c AppDbContext -s ..\SystemyWP.API -o .\Migrations

dotnet ef migrations script -i -c AppDbContext -s ..\SystemyWP.API -o script.sql

### identity migrations & update

dotnet ef migrations add <name> -c ApiIdentityDbContext -o .\IdentityMigrations update

dotnet ef migrations script -i -c ApiIdentityDbContext -o .\Scripts\script.sql

### just update

dotnet ef database update -c AppDbContext

dotnet ef database update -c ApiIdentityDbContext

dotnet ef migrations remove -c AppDbContext -s ../SystemyWP.API


