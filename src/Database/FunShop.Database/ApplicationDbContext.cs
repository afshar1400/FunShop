using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunShop.Database
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            // for seeding database



            string adminId = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
            string adminRoleId = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";
            string userRoleId = "341743f0-asd2–43ee-afbf-59kmkkmk72cf6";

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "admin",
                NormalizedName = "admin".ToUpper(),
                Id = adminRoleId,
                ConcurrencyStamp = adminRoleId
            });

            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "user", NormalizedName = "user".ToUpper(), Id = userRoleId, ConcurrencyStamp = userRoleId });


            IdentityUser admin = new IdentityUser { UserName = "admin", NormalizedUserName = "admin".ToUpper(), Email = "admin@gmail.com", NormalizedEmail = "admin@gmail.com".ToUpper(), Id = adminId };
            // set user password


            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            admin.PasswordHash = ph.HashPassword(admin, "123456");

            builder.Entity<IdentityUser>().HasData(admin);
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { RoleId = adminRoleId, UserId = adminId });
        }
    }
}
