using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnduraGenius.API.Migrations
{
    /// <inheritdoc />
    public partial class fixnotnull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_AspNetUsers_PlanCreatedBy",
                table: "Plans");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_AspNetUsers_WorkoutCreatedBy",
                table: "Workouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Muscles_MainMuscleId",
                table: "Workouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Muscles_SecondaryMuscleId",
                table: "Workouts");

            migrationBuilder.AlterColumn<string>(
                name: "WorkoutCreatedBy",
                table: "Workouts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "MainMuscleId",
                table: "Workouts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "PlanCreatedBy",
                table: "Plans",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_AspNetUsers_PlanCreatedBy",
                table: "Plans",
                column: "PlanCreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_AspNetUsers_WorkoutCreatedBy",
                table: "Workouts",
                column: "WorkoutCreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Muscles_MainMuscleId",
                table: "Workouts",
                column: "MainMuscleId",
                principalTable: "Muscles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Muscles_SecondaryMuscleId",
                table: "Workouts",
                column: "SecondaryMuscleId",
                principalTable: "Muscles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_AspNetUsers_PlanCreatedBy",
                table: "Plans");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_AspNetUsers_WorkoutCreatedBy",
                table: "Workouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Muscles_MainMuscleId",
                table: "Workouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Muscles_SecondaryMuscleId",
                table: "Workouts");

            migrationBuilder.AlterColumn<string>(
                name: "WorkoutCreatedBy",
                table: "Workouts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MainMuscleId",
                table: "Workouts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PlanCreatedBy",
                table: "Plans",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_AspNetUsers_PlanCreatedBy",
                table: "Plans",
                column: "PlanCreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_AspNetUsers_WorkoutCreatedBy",
                table: "Workouts",
                column: "WorkoutCreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
    }
}
