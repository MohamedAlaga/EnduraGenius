using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnduraGenius.API.Migrations
{
    /// <inheritdoc />
    public partial class addorderanddaystoplanworkoutstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayNumber",
                table: "PlanWorkouts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "PlanWorkouts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayNumber",
                table: "PlanWorkouts");

            migrationBuilder.DropColumn(
                name: "order",
                table: "PlanWorkouts");
        }
    }
}
