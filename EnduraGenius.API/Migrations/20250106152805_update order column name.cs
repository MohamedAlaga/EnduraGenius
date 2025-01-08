using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnduraGenius.API.Migrations
{
    /// <inheritdoc />
    public partial class updateordercolumnname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "order",
                table: "PlanWorkouts",
                newName: "Order");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Order",
                table: "PlanWorkouts",
                newName: "order");
        }
    }
}
