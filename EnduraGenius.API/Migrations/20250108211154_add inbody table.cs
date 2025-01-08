using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnduraGenius.API.Migrations
{
    /// <inheritdoc />
    public partial class addinbodytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "AspNetUsers",
                newName: "WeightInKg");

            migrationBuilder.RenameColumn(
                name: "Waist",
                table: "AspNetUsers",
                newName: "TallInCm");

            migrationBuilder.RenameColumn(
                name: "Tall",
                table: "AspNetUsers",
                newName: "Age");

            migrationBuilder.AddColumn<bool>(
                name: "IsMale",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMale",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "WeightInKg",
                table: "AspNetUsers",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "TallInCm",
                table: "AspNetUsers",
                newName: "Waist");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "AspNetUsers",
                newName: "Tall");
        }
    }
}
