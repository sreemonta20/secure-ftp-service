using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace secure_ftp_service.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FileCreationime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SourceFilePath = table.Column<string>(type: "text", nullable: false),
                    DestinationFilePath = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileDetails");
        }
    }
}
