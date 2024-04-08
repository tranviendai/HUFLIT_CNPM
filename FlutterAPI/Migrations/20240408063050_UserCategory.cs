using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlutterAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Category",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "20DH111558",
                columns: new[] { "BirthDay", "ConcurrencyStamp", "DateCreated", "SecurityStamp" },
                values: new object[] { new DateTime(2024, 4, 7, 23, 30, 49, 200, DateTimeKind.Local).AddTicks(3188), "09d3be16-de16-4dff-97ac-ab26ab8ac0d6", new DateTime(2024, 4, 7, 23, 30, 49, 200, DateTimeKind.Local).AddTicks(3186), "cd1e23c8-5b89-48c2-9704-b1ae1ce0dd9a" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "Admin",
                columns: new[] { "BirthDay", "ConcurrencyStamp", "DateCreated", "SecurityStamp" },
                values: new object[] { new DateTime(2024, 4, 7, 23, 30, 49, 200, DateTimeKind.Local).AddTicks(3101), "b8a4faa3-7efc-4629-8274-2a097351f7de", new DateTime(2024, 4, 7, 23, 30, 49, 200, DateTimeKind.Local).AddTicks(3162), "12f6a028-93e0-4af9-8010-7d82435ce1a7" });

            migrationBuilder.CreateIndex(
                name: "IX_Category_UserID",
                table: "Category",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_User_UserID",
                table: "Category",
                column: "UserID",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_User_UserID",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_UserID",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Category");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "20DH111558",
                columns: new[] { "BirthDay", "ConcurrencyStamp", "DateCreated", "SecurityStamp" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 17, 14, 262, DateTimeKind.Local).AddTicks(5962), "a152686c-9089-44e2-9d7e-a345def122cc", new DateTime(2024, 4, 3, 1, 17, 14, 262, DateTimeKind.Local).AddTicks(5960), "d785e8ed-f206-41d0-811a-2870eae6488e" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "Admin",
                columns: new[] { "BirthDay", "ConcurrencyStamp", "DateCreated", "SecurityStamp" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 17, 14, 262, DateTimeKind.Local).AddTicks(5916), "211396cf-02d9-484a-99a3-3424a3ba46d6", new DateTime(2024, 4, 3, 1, 17, 14, 262, DateTimeKind.Local).AddTicks(5936), "0d058441-4f66-49fc-9165-407f65d1567b" });
        }
    }
}
