using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnduraGenius.API.Migrations
{
    /// <inheritdoc />
    public partial class fixtimesPerformedcolumnlocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimesPerformed",
                table: "PlanWorkouts");

            migrationBuilder.AddColumn<int>(
                name: "TimesPerformed",
                table: "UserWorkouts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimesPerformed",
                table: "UserWorkouts");

            migrationBuilder.AddColumn<int>(
                name: "TimesPerformed",
                table: "PlanWorkouts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
