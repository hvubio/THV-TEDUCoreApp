using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeduCoreApp.Data.EF.Configurations;
using TeduCoreApp.Data.EF.Extentions;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Data.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        #region DbSet

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<AdvertisementPage> AdvertisementPages { get; set; }
        public DbSet<AdvertisementPosition> AdvertisementPositions { get; set; }
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
            

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Identity config

            builder.Entity<IdentityUserClaim<string>>().ToTable("AppUserClaim").HasKey(c => c.Id);
            builder.Entity<IdentityRoleClaim<string>>().ToTable("AppRoleClaim").HasKey(c => c.Id);
            builder.Entity<IdentityUserLogin<string>>().ToTable("AppUserLogin").HasKey(c => c.UserId);
            builder.Entity<IdentityUserRole<string>>().ToTable("AppUserRole").HasKey(c => new {c.RoleId, c.UserId});
            builder.Entity<IdentityUserToken<string>>().ToTable("AppUserToken").HasKey(c => new {c.UserId});

            #endregion

            

            builder.AddConfiguration(new AdvertisementPositionConfiguration());
            builder.AddConfiguration(new ContactDetailsConfiguration());
            builder.AddConfiguration(new FooterConfiguration());
            builder.AddConfiguration(new PageConfiguration());
            builder.AddConfiguration(new ProductTagConfiguration());
            builder.AddConfiguration(new SystemConfigConfiguration());
            builder.AddConfiguration(new TagConfiguration());

            base.OnModelCreating(builder);
        }
    }
}