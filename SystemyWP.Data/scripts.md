###migrations
dotnet ef migrations add AppDbContext -c AppDbContext -s ..\SystemyWP.API -o .\Migrations

###migration script
dotnet ef migrations script -i -c AppDbContext -s ..\SystemyWP.API -o script.sql

###identity migrations & update
dotnet ef migrations add ApiIdentityDbContext -c ApiIdentityDbContext -o .\IdentityMigrations
dotnet ef migrations script -i -c ApiIdentityDbContext -o script.sql
dotnet ef database update -c ApiIdentityDbContext