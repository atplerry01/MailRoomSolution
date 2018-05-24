using MailRoom.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MailRoom.Api.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
           
            // modelBuilder.Entity<JobManifestLog>()
            //     .HasKey(bc => new { bc.JobManifestId, bc.JobManifestBranchId });

            // modelBuilder.Entity<JobManifestLog>()
            //         .HasOne(bc => bc.JobManifest)
            //         .WithMany(b => b.JobManifestLogs)
            //         .HasForeignKey(bc => bc.JobManifestId);

            // modelBuilder.Entity<JobManifestLog>()
            //         .HasOne(bc => bc.JobManifestBranch)
            //         .WithMany(c => c.JobManifestLogs)
            //         .HasForeignKey(bc => bc.JobManifestBranchId);

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<CardCustodian> CardCustodians { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientBranch> ClientBranches { get; set; }
        public DbSet<ClientHeadQuarter> ClientHeadQuarters { get; set; }
        public DbSet<JobData> Jobdatas { get; set; }
        public DbSet<JobManifest> JobManifests { get; set; }
        public DbSet<JobManifestBranch> JobManifestBranches { get; set; }
        public DbSet<JobManifestLog> JobManifestLogs { get; set; }

    }
}