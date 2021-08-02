### migrations

dotnet ef migrations add <name> -c AppDbContext -s ..\SystemyWP.Data -o .\Migrations

### migration script -- full migration every time

dotnet ef migrations script -i -c AppDbContext -s ..\SystemyWP.API -o script.sql

### identity migrations & update

dotnet ef migrations add <name> -c ApiIdentityDbContext -o .\IdentityMigrations

dotnet ef migrations script -i -c ApiIdentityDbContext -o .\Scripts\script.sql

### just update

dotnet ef database update -c ApiIdentityDbContext







