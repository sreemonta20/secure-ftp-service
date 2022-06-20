## Procedure to run the project (.NET 6)

### Compatibility Check

Open Package Manager Console

- Run `dotnet restore`

Close the manager console and clear and build the project

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

### Create the Database (Code First Approach)

Open Package Manager Console

- Run `enable-migrations`  // Enable-Migrations is obsolete now for .NET 6. Start from the below
- Run `Add-Migration initial`
- Run `update-database`

### Procedure to run the project (Example)  ****

- Download the project from github (https://github.com/sreemonta20/secure-ftp-service.git) (1)
- Keep the project in a suitable location in your computer 
- Download Rebex Tiny SFTP Server (free) from the link https://www.rebex.net/tiny-sftp-server/#download
- Keep the downloaded rebex tiny server software in a suitable location of your computer.
- Create a folder (named "public") inside the data folder of rebex tiny server software. Which will be act as a server folder. Where we can keep files.
- Create a archieves named folder in a suitable location of your computer. This will be act as a local folder to receieve the files from the server folder.
- Now click on the tiny server icon, copy all the attrbutes such as host ip, port, username, password, and other details(if needed)
- Now configure appsettings.json file where you can paste all the values you copied previously. 
  Also remember the mention or paste the 3 path locations -
  1. LocalPathDirectory: the path location of your local folder (such as archieves or any other folder name).
  2. ServerPathBaseDirectory:  Base path location (till the data folder , which is created by by default by tiny server soft).
  3. ServerPathDirectory: such as public folder location. (ex: \\public).
- Click the start button on the server soft.
- Run `dotnet restore` in the package manager console of the downloaded project. Then clear, build, and run the project.
- Run `Add-Migration initial` && `update-database`. And build once again if it is necessary.
- You can keep the several files in the sample server folder(\\public). See the downloaded files in the local folder and information regarding the downloaded
  files in the database

- Download the Sample tiny server in zipped folder (tiny server) (https://github.com/sreemonta20/tiny-server-and-archieves/blob/main/rebex_tiny_sftp_server.zip) (2) 
- Download the archieves folder in zipped folder (localfolder) (https://github.com/sreemonta20/tiny-server-and-archieves/blob/main/archieves.zip) (3)



