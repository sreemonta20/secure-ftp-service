using Microsoft.EntityFrameworkCore;
using secure_ftp_service.Core.Models;

namespace secure_ftp_service.Persistence.ORM
{
    public class SFTPDbContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public SFTPDbContext(DbContextOptions<SFTPDbContext> options) : base(options) { 

        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DbSet<SftpFileDetails> FileDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<SftpFileDetails>().Property(e => e.Id).ValueGeneratedOnAdd();
            

        }

        

    }
}
