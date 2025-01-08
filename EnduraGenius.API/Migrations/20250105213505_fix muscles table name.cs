using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnduraGenius.API.Migrations
{
    /// <inheritdoc />
    public partial class fixmusclestablename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_muscles_MainMuscleId",
                table: "Workouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_muscles_SecondaryMuscleId",
                table: "Workouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_muscles",
                table: "muscles");

            migrationBuilder.RenameTable(
                name: "muscles",
                newName: "Muscles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Muscles",
                table: "Muscles",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de6594c7-64c1-4d22-bdfc-4de7eda3628c",
                column: "Name",
                value: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Muscles_MainMuscleId",
                table: "Workouts",
                column: "MainMuscleId",
                principalTable: "Muscles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Muscles_SecondaryMuscleId",
                table: "Workouts",
                column: "SecondaryMuscleId",
                principalTable: "Muscles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Muscles_MainMuscleId",
                table: "Workouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Muscles_SecondaryMuscleId",
                table: "Workouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Muscles",
                table: "Muscles");

            migrationBuilder.RenameTable(
                name: "Muscles",
                newName: "muscles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_muscles",
                table: "muscles",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de6594c7-64c1-4d22-bdfc-4de7eda3628c",
                column: "Name",
                value: "user");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_muscles_MainMuscleId",
                table: "Workouts",
                column: "MainMuscleId",
                principalTable: "muscles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_muscles_SecondaryMuscleId",
                table: "Workouts",
                column: "SecondaryMuscleId",
                principalTable: "muscles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
