using Microsoft.EntityFrameworkCore.Migrations;

namespace src.Migrations
{
    public partial class AddUserAndRoleSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "0b610f38-ce80-4973-8273-7fd4e0b289fb", "Admin", "ADMIN" },
                    { 2, "d1512979-73d8-49a9-ae20-e2b56663db45", "Staff", "STAFF" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Firstname", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "849cfc87-6543-4aa9-bd38-b9c9cbbf3f17", null, false, null, null, false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEMi4O/4zNZSpuEiVdpaJrt2dmWjRteyDWmJJooM7zZ/HUnyg473bE4ZKG19Hhqo78A==", null, false, null, false, "admin" },
                    { 2, 0, "d43518a7-d4a9-48de-95bb-593532b53efa", null, false, null, null, false, null, null, "STAFF", "AQAAAAEAACcQAAAAEEs2UcsxzrQlaXDeLHuehYoyGZTvZzfq00ACsm9pr3NVvbcT5dNRwppPtsWPxcvUmQ==", null, false, null, false, "staff" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { 2, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
