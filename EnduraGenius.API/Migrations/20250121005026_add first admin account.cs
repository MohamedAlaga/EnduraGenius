using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EnduraGenius.API.Migrations
{
    /// <inheritdoc />
    public partial class addfirstadminaccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsMale", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Points", "ProfilePicture", "SecurityStamp", "TallInCm", "TwoFactorEnabled", "UserName", "WeightInKg", "isPublic" },
                values: new object[] { "c43b848e-de8a-448a-87f0-354e9f505272", 0, 22, "f02a0229-1fa7-4c12-8985-2436ff1b495b", "admin2@EnduraGenius.com", false, true, true, null, "ADMIN2@ENDURAGENIUS.COM", "ALAGA_ADMIN2", "AQAAAAIAAYagAAAAEB8mSwn38vSJ7/KNpsmlbKq6WJrDHdhP0KFAIf+XSJ4OxMtfq7eqdLrv3IxTrqbSQg==", null, false, 0, null, "FJAS5D5VKCP3AVUILWGTTBJWK2TLWS36", 182, false, "Alaga_Admin2", 95f, true });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "de6594c7-64c1-4d22-bdfc-4de7eda3628c", "c43b848e-de8a-448a-87f0-354e9f505272" },
                    { "f02a0229-1fa7-4c12-8985-2436ff1b495b", "c43b848e-de8a-448a-87f0-354e9f505272" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "de6594c7-64c1-4d22-bdfc-4de7eda3628c", "c43b848e-de8a-448a-87f0-354e9f505272" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f02a0229-1fa7-4c12-8985-2436ff1b495b", "c43b848e-de8a-448a-87f0-354e9f505272" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c43b848e-de8a-448a-87f0-354e9f505272");
        }
    }
}
