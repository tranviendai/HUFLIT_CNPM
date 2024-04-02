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
        const string ADMIN_ROLE_GUID = "Admin";
        const string STUDENT_ROLE_GUID = "Student";
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
                PasswordHash = "baotran@910",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
                NormalizedUserName = "admin@gmail.com".ToUpper(),
                EmailConfirmed = true,
                FullName = "Admin",
                PhoneNumber = "",
                Active = true,
                Gender = "Nữ",
                BirthDay = DateTime.Now,
                DateCreated = DateTime.Now,
            },
            new User
            {
                Id = "20DH111558",
                UserName = "20DH111558",
                Email = "student@gmail.com",
                PasswordHash = "123456",
                NormalizedEmail = "student@gmail.com".ToUpper(),
                NormalizedUserName = "student@gmail.com".ToUpper(),
                EmailConfirmed = true,
                FullName = "Trần Viễn Đại",
                PhoneNumber = "0582072743",
                DateCreated = DateTime.Now,
                Gender = "Nam",
                BirthDay = DateTime.Now,
                Active = true,
                SchoolKey = "",
                ImageURL = "https://scontent.fsgn6-1.fna.fbcdn.net/v/t1.6435-9/118910515_121133139718352_4296691441835191434_n.jpg?_nc_cat=103&ccb=1-7&_nc_sid=5f2048&_nc_ohc=gATiW_XjmGcAX_N8wGa&_nc_ht=scontent.fsgn6-1.fna&oh=00_AfAGkhL-KyyMkOWqQ_GJIbIfgwtPON7S3QUoyD_8LDvjRw&oe=66337DE7"
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
        public DbSet<Bill> Bill { get; set; }
        public DbSet<Order> Order { get; set; }
    }
}
