using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using QuasarLight.Common.Contracts.Data;
using QuasarLight.Common.Contracts.Repositories;
using QuasarLight.Data.Entity.Repositories;
using QuasarLight.Data.Model.DataModel;

namespace QuasarLight.Data.Entity
{
    public class ApplicationDbContext : IdentityDbContext<User>, IDataContext
    {
        public ApplicationDbContext() : base("MainConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDbContext>());
        }

        //public DbSet<UserImage> UserImages { get; set; }
        public virtual ICollection<IdentityUserRole> UserRoles { get; private set; }
        public virtual ICollection<IdentityUserClaim> Claims { get; private set; }
        public virtual ICollection<IdentityUserLogin> Logins { get; private set; }

        IUserRepository IDataContext.Users => new UserRepository(this);
        IUserImageRepository IDataContext.UserImages => new UserImageRepository(this);
        IImageRepository IDataContext.Images => new ImageRepository(this);


        void IDataContext.Commit()
        {
            SaveChanges();
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<User>()
                .HasMany(s => s.UserImages)
                    .WithRequired(s => s.User)
                    .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<UserImage>()
                    .HasRequired(s => s.User)
                    .WithMany(s => s.UserImages)
                    .HasForeignKey(s => s.UserId);


            modelBuilder.Entity<IdentityUserRole>()
            .HasKey(r => new { r.UserId, r.RoleId })
            .ToTable("UserRoles");

            modelBuilder.Entity<IdentityUserLogin>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId })
                .ToTable("UserLogins");

            modelBuilder.Entity<IdentityUserClaim>()
                .ToTable("UserClaims");
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}