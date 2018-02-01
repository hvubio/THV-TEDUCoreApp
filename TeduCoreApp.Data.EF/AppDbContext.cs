using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using TeduCoreApp.Data.EF.Configurations;
using TeduCoreApp.Data.EF.Extentions;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Interfaces;

namespace TeduCoreApp.Data.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        #region DbSet

        public DbSet<Advertistment> Advertistments { get; set; }
        public DbSet<AdvertistmentPage> AdvertistmentPages { get; set; }
        public DbSet<AdvertistmentPosition> AdvertistmentPositions { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<AnnouncementUser> AnnouncementUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Permission> Permissions { get; set; }  
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductColor> PropColors { get; set; }                 
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductQuantity> PropQuantities { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<WholePrice> WholePrices { get; set; }
            

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Identity config

            builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaim").HasKey(c => c.Id);
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaim").HasKey(c => c.Id);
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogin").HasKey(c => c.UserId);
            builder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRole").HasKey(c => new {c.RoleId, c.UserId});
            builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserToken").HasKey(c => new {c.UserId});

            #endregion

            builder.AddConfiguration(new AdvertisementPositionConfiguration());
            builder.AddConfiguration(new ContactDetailsConfiguration());
            builder.AddConfiguration(new FooterConfiguration());
            builder.AddConfiguration(new FunctionConfiguration());
            builder.AddConfiguration(new PageConfiguration());
            builder.AddConfiguration(new ProductTagConfiguration());
            builder.AddConfiguration(new SystemConfigConfiguration());
            builder.AddConfiguration(new TagConfiguration());

            //base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            var modified = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach (var entityEntry in modified)
            {
                if (!(entityEntry.Entity is IDateTracking changeOrAddedItem)) continue;
                if (entityEntry.State == EntityState.Added)
                {
                    changeOrAddedItem.DateCreated = DateTime.Now;       
                }
                changeOrAddedItem.DateModified = DateTime.Now;

            }
            return base.SaveChanges();
        }

        public class DesignTimeDbContextFactory: IDesignTimeDbContextFactory<AppDbContext>
        {
            public AppDbContext CreateDbContext(string[] args)
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
                var builder = new DbContextOptionsBuilder<AppDbContext>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                builder.UseSqlServer(connectionString);
                return new AppDbContext(builder.Options);
            }
        }
    }
}