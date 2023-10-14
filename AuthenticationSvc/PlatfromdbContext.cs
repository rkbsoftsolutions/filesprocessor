using AuthenticationSvc.Entities;
using AuthenticationSvc.IdentityClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationSvc
{
    public class UserPlatfromdbContext : IdentityDbContext<ApplicationUsers, ApplicationRoles,
        Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>,
        IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public UserPlatfromdbContext(DbContextOptions<UserPlatfromdbContext> dbContextOptions) : base(dbContextOptions)
        {
            this.Database.EnsureCreated();
        }


        public DbSet<ApplicationUsers> users { get; set; }

        public DbSet<ApplicationRoles> applicationRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUsers>().ToTable("AspNetUsers");
            builder.Entity<ApplicationRoles>().ToTable("AspNetRoles");
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            //    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            //if (!await roleManager.RoleExistsAsync(UserRoles.User))
            //    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            //if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            //{
            //    await userManager.AddToRoleAsync(user, UserRoles.Admin);
            //}
        }
      
    }

    public class PlatfromdbContext : DbContext
    {
        public PlatfromdbContext(DbContextOptions<PlatfromdbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<NewsEntity> news { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntries = ChangeTracker.Entries<ITenant>();
            foreach(var e in entityEntries)
            {
                if(e.State == EntityState.Added)
                {
                    e.Entity.CreateDdate = DateTime.UtcNow;
                }
                else if (e.State == EntityState.Modified)
                {
                    e.Property(p => p.CreateDdate).IsModified = false;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<NewsEntity>((e) =>
            {
                e.HasKey((p) => p.Id);
                e.Property(p => p.FullDetail).HasColumnName("Full");
                e.Property(p => p.ShortDetail).HasColumnName("Short");
                e.Property(p => p.Title).HasColumnName("Title");
                e.ToTable("News");
            }



            );
            base.OnModelCreating(builder);
        }
    }
}
   
    
