using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reduvius.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reduvius.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //public override int SaveChanges()
        //{
        //    var entries = ChangeTracker
        //        .Entries()
        //        .Where(e => e.Entity is BaseEntity && (
        //            e.State == EntityState.Added
        //            || e.State == EntityState.Modified
        //        ));

        //    foreach (var entityEntry in entries)
        //    {
        //        ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

        //        if (entityEntry.State == EntityState.Added)
        //        {
        //            ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
        //        }
        //    }


        //    return base.SaveChanges();
        //}

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entries = ChangeTracker.Entries()
                                       .Where(e => e.Entity is BaseEntity && (
                                       e.State == EntityState.Added 
                                       || e.State == EntityState.Modified
                                       ));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProject>().HasKey(up => new { up.UserId, up.ProjectId });

            modelBuilder.Entity<Mini>()
                .HasOne<Project>(m => m.Project)
                .WithMany(p => p.Minis)
                .HasForeignKey(m => m.ProjectId);

            modelBuilder.Entity<Bug>()
                .HasOne<Mini>(b => b.Mini)
                .WithMany(m => m.Bugs)
                .HasForeignKey(b => b.MiniId);

            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Mini> Minis { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
    }
}
