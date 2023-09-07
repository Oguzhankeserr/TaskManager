
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Identity.Domain.Entities;

namespace TaskManager.Identity.Infrastructure.Context
{
    public class TaskManagerDbContext : IdentityDbContext<AppUser,AppRole, string>
    {
        public TaskManagerDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppRole>().ToTable("aspnetroles");
            builder.Entity<AppUser>().ToTable("aspnetusers");
            //builder.Entity<AppClaim>().ToTable("aspnetroleclaims");
            //builder.Entity<AppUserClaim>().ToTable("aspnetuserclaims");
            //builder.Entity<AppUserLogin>().ToTable("aspnetuserlogins");
            //builder.Entity<AppUserRole>().ToTable("aspnetuserroles");
            //builder.Entity<AppUserToken>().ToTable("aspnetusertokens");

            this.SeedUsers(builder);
            this.SeedRoles(builder);
            this.SeedUserRoles(builder);
        }

        private void SeedUsers(ModelBuilder builder)
        {
          
            AppUser user = new AppUser()
            {
                Id = "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                UserName = "super.admin",
                Name = "Super",
                Surname = "Admin",
                Email = "superadmin@gmail.com",
                LockoutEnabled = false,
                NormalizedUserName = "SUPERADMIN",
                NormalizedEmail = "SUPERADMIN@GMAIL.COM"
            };

            PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "superadmin123");

            builder.Entity<AppUser>().HasData(user);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<AppRole>().HasData(
                new AppRole() { Id = "6a2c4fe5-5b10-45b6-a1f6-7cfecc629d3f", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                new AppRole() { Id = "336e1648-5384-4d2c-b886-0281db620ccb", Name = "User", ConcurrencyStamp = "2", NormalizedName = "USER" } ,
                new AppRole() { Id = "4dc5874d-f3be-459a-b05f-2244512d13e3", Name = "SuperAdmin", ConcurrencyStamp = "3", NormalizedName = "SUPERADMIN" }
                );
        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "4dc5874d-f3be-459a-b05f-2244512d13e3", UserId = "94c328af-952d-42a5-ae86-4f0fe6d84d74" }
                );
        }
    }
}


