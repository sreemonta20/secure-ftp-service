## Procedure to run the project

### Compatibility Check

Open Package Manager Console

- Run `dotnet restore`

Close the manager console and clear and build the project


### Create the Database (Code First Approach)

Open Package Manager Console

- Run `enable-migrations`  // Enable-Migrations is obsolete now for .NET 6. Start from the below
- Run `Add-Migration initial`
- Run `update-database`


### Install Serilog (for reading and tracking the log in file, console, and centralized logging solution)

Open Package Manager Console

- Run `Install-Package Serilog -Version 2.11.0`  
- Run `Install-Package Serilog.AspNetCore -Version 2.1.1`
- Run `Install-Package Serilog.Sinks.Console -Version 3.1.1`
- Run `Install-Package Serilog.Sinks.File -Version 5.0.1-dev-00947`
- Run `Install-Package Serilog.Sinks.Graylog -Version 2.3.0`
- Run `Install-Package Serilog.Extensions.Hosting -Version 2.0.0`
- Run `Install-Package Serilog.Exceptions -Version 8.2.0`
- Run `Install-Package Serilog.Enrichers.Environment -Version 2.2.1-dev-00787`

### Install PostgreSql Packages & Dependencies

Open Package Manager Console

- Run `Install-Package EFCore.BulkExtensions -Version 6.5.1`
- Run `Install-Package Npgsql.EntityFrameworkCore.PostgreSQL -Version 7.0.0-preview.4`
- Run `Install-Package Microsoft.EntityFrameworkCore.Design -Version 7.0.0-preview.5.22302.2`
- Run `Install-Package Microsoft.EntityFrameworkCore.Tools -Version 7.0.0-preview.5.22302.2`
- Run `Install-Package Microsoft.EntityFrameworkCore -Version 7.0.0-preview.5.22302.2`
