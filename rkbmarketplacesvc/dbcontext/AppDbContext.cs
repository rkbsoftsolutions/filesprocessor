using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReferigenatorSvc.dbcontext
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options):base(options)
        {
            this.Database?.EnsureCreated();

        }
        public DbSet<ItemHistoryEntity> itemHistoryEntities { get; set; }

       public DbSet<ItemsEntity> itemsEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemHistoryEntity>()
            .HasOne<ItemsEntity>(s => s.ItemsEntity)
            .WithMany(g => g.ItemHistoryEntities)
            .HasForeignKey(s => s.ItemId);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entityEntry in ChangeTracker.Entries<EntityBase>())
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Entity.CreateDate = DateTime.UtcNow;
                    entityEntry.Entity.IsActive = true;
                }
                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Entity.UpdateDate = DateTime.UtcNow;
                    entityEntry.Entity.IsActive = true;
                    entityEntry.Property(p => p.IsActive).IsModified = true;
                    entityEntry.Property(p => p.CreateDate).IsModified = false;
                }
                if (entityEntry.State == EntityState.Deleted)
                {
                    entityEntry.Entity.UpdateDate = DateTime.UtcNow;
                    entityEntry.Entity.IsActive = false;
                    entityEntry.Property(p => p.IsActive).IsModified = true;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }

}
