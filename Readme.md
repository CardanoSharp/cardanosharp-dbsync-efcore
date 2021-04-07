## Update Models to new `cardano-db-sync` version 

### Requirements

Install `dotnet-ef` tools

cli: `dotnet tool install --global dotnet-ef`

Also requires an app with `Microsoft.EntityFrameworkCore.Design`. Will update repo later to include such an app to make re-scaffolding models easier.

### Steps

 - Delete `Models` folder

 - cli: `dotnet ef dbcontext scaffold "User ID=DB_USER;Password=DB_PASS;Host=DB_HOST;Port=DB_PORT;Database=DB_NAME;" Npgsql.EntityFrameworkCore.PostgreSQL -o path/to/models/ -n CardanoSharp.DbSync.EntityFramework.Models --schema public`