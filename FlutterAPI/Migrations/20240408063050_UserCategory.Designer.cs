﻿// <auto-generated />
using System;
using FlutterAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlutterAPI.Migrations
{
    [DbContext(typeof(FlutterAPIContext))]
    [Migration("20240408063050_UserCategory")]
    partial class UserCategory
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FlutterAPI.Model.Bill", b =>
                {
                    b.Property<string>("BillID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("NumberID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("BillID");

                    b.HasIndex("NumberID");

                    b.ToTable("Bill");
                });

            modelBuilder.Entity("FlutterAPI.Model.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("FlutterAPI.Model.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BillID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int?>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BillID");

                    b.HasIndex("ProductID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("FlutterAPI.Model.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CategoryID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("FlutterAPI.Model.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NumberID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SchoolKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchoolYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("PhoneNumber")
                        .IsUnique()
                        .HasFilter("[PhoneNumber] IS NOT NULL");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "Admin",
                            AccessFailedCount = 0,
                            Active = true,
                            BirthDay = new DateTime(2024, 4, 7, 23, 30, 49, 200, DateTimeKind.Local).AddTicks(3101),
                            ConcurrencyStamp = "b8a4faa3-7efc-4629-8274-2a097351f7de",
                            DateCreated = new DateTime(2024, 4, 7, 23, 30, 49, 200, DateTimeKind.Local).AddTicks(3162),
                            Email = "admin@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Admin",
                            Gender = "Nữ",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN@GMAIL.COM",
                            PasswordHash = "baotran@910",
                            PhoneNumber = "",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "12f6a028-93e0-4af9-8010-7d82435ce1a7",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = "20DH111558",
                            AccessFailedCount = 0,
                            Active = true,
                            BirthDay = new DateTime(2024, 4, 7, 23, 30, 49, 200, DateTimeKind.Local).AddTicks(3188),
                            ConcurrencyStamp = "09d3be16-de16-4dff-97ac-ab26ab8ac0d6",
                            DateCreated = new DateTime(2024, 4, 7, 23, 30, 49, 200, DateTimeKind.Local).AddTicks(3186),
                            Email = "student@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Trần Viễn Đại",
                            Gender = "Nam",
                            ImageURL = "https://scontent.fsgn6-1.fna.fbcdn.net/v/t1.6435-9/118910515_121133139718352_4296691441835191434_n.jpg?_nc_cat=103&ccb=1-7&_nc_sid=5f2048&_nc_ohc=gATiW_XjmGcAX_N8wGa&_nc_ht=scontent.fsgn6-1.fna&oh=00_AfAGkhL-KyyMkOWqQ_GJIbIfgwtPON7S3QUoyD_8LDvjRw&oe=66337DE7",
                            LockoutEnabled = false,
                            NormalizedEmail = "STUDENT@GMAIL.COM",
                            NormalizedUserName = "STUDENT@GMAIL.COM",
                            PasswordHash = "123456",
                            PhoneNumber = "0582072743",
                            PhoneNumberConfirmed = false,
                            SchoolKey = "",
                            SecurityStamp = "cd1e23c8-5b89-48c2-9704-b1ae1ce0dd9a",
                            TwoFactorEnabled = false,
                            UserName = "20DH111558"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "Admin",
                            Name = "Admin",
                            NormalizedName = "Admin"
                        },
                        new
                        {
                            Id = "Student",
                            Name = "Student",
                            NormalizedName = "Student"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "Admin",
                            RoleId = "Admin"
                        },
                        new
                        {
                            UserId = "20DH111558",
                            RoleId = "Student"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("FlutterAPI.Model.Bill", b =>
                {
                    b.HasOne("FlutterAPI.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("NumberID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FlutterAPI.Model.Category", b =>
                {
                    b.HasOne("FlutterAPI.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FlutterAPI.Model.Order", b =>
                {
                    b.HasOne("FlutterAPI.Model.Bill", "Bill")
                        .WithMany("Orders")
                        .HasForeignKey("BillID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlutterAPI.Model.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");

                    b.Navigation("Bill");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("FlutterAPI.Model.Product", b =>
                {
                    b.HasOne("FlutterAPI.Model.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryID");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FlutterAPI.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FlutterAPI.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlutterAPI.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FlutterAPI.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FlutterAPI.Model.Bill", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("FlutterAPI.Model.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}