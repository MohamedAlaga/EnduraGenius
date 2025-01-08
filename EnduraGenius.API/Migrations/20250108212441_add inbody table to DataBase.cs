using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnduraGenius.API.Migrations
{
    /// <inheritdoc />
    public partial class addinbodytabletoDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inbodies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    weight = table.Column<float>(type: "real", nullable: false),
                    BMI = table.Column<float>(type: "real", nullable: false),
                    BMR = table.Column<float>(type: "real", nullable: false),
                    FFM = table.Column<float>(type: "real", nullable: false),
                    LBM = table.Column<float>(type: "real", nullable: false),
                    BFP = table.Column<float>(type: "real", nullable: false),
                    TBW = table.Column<float>(type: "real", nullable: false),
                    CaloricNeed = table.Column<int>(type: "int", nullable: false),
                    WaterIntake = table.Column<float>(type: "real", nullable: false),
                    IdealBodyWeight = table.Column<float>(type: "real", nullable: false),
                    DailyProtenNeedInGrams = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inbodies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inbodies_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inbodies_userId",
                table: "Inbodies",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inbodies");
        }
    }
}
