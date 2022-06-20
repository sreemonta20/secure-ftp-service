using Microsoft.EntityFrameworkCore;
using secure_ftp_service.Core.Models;

namespace secure_ftp_service.Persistence.ORM
{
    /// <summary>
    /// It acts as the FTP database context of Entity Framework Core for the Postgresql database. 
    /// </summary>
    public class SFTPDbContext : DbContext
    {
        public SFTPDbContext(DbContextOptions<SFTPDbContext> options) : base(options) { 

        }
        public DbSet<SftpFileDetails> FileDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<SftpFileDetails>().Property(e => e.Id).ValueGeneratedOnAdd();
            

        }

        

    }
}
