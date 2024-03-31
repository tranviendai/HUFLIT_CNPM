using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlutterAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FlutterAPI.Data
{
    public class FlutterAPIContext : IdentityDbContext<User>
    {
        const string ADMIN_USER_GUID = "Admin";
        const string ADMIN_ROLE_GUID = "ADMIN-ROLE";
        const string STUDENT_ROLE_GUID = "STUDENT-ROLE";
        private User user;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
                entity.HasData(
                    new IdentityRole
                    {
                        Id = ADMIN_ROLE_GUID,
                        Name = "Admin",
                        NormalizedName = "Admin"
                    },
                    new IdentityRole
                    {
                        Id = STUDENT_ROLE_GUID,
                        Name = "Student",
                        NormalizedName = "Student"
                    }
                   );
            });
            var hasher = new PasswordHasher<User>();
            builder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasData(
            new User
            {
                Id = ADMIN_USER_GUID,
                UserName = "admin",
                Email = "admin@gmail.com",
                PasswordHash = hasher.HashPassword(user, "123456"),
                NormalizedEmail = "admin@gmail.com".ToUpper(),
                NormalizedUserName = "admin@gmail.com".ToUpper(),
                EmailConfirmed = true,
                FullName = "Admin",
                PhoneNumber = "",
                DateCreated = DateTime.Now
            },
            new User
            {
                Id = "20DH111558",
                UserName = "20DH111558",
                Email = "student@gmail.com",
                PasswordHash = hasher.HashPassword(user, "123456"),
                NormalizedEmail = "student@gmail.com".ToUpper(),
                NormalizedUserName = "student@gmail.com".ToUpper(),
                EmailConfirmed = true,
                FullName = "Trần Viễn Đại",
                PhoneNumber = "0582072743",
                DateCreated = DateTime.Now
            });;

            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
                entity.HasData(
                    new IdentityUserRole<string>
                    {
                        RoleId = ADMIN_ROLE_GUID,
                        UserId = ADMIN_USER_GUID,
                    },
                    new IdentityUserRole<string>
                    {
                        RoleId = STUDENT_ROLE_GUID,
                        UserId = "20DH111558",
                    }
                );
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
        }
        public FlutterAPIContext (DbContextOptions<FlutterAPIContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }

    }
}
