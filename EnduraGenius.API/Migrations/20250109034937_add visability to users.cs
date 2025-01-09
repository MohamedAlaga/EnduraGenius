using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnduraGenius.API.Migrations
{
    /// <inheritdoc />
    public partial class addvisabilitytousers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isPublic",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isPublic",
                table: "AspNetUsers");
        }
    }
}
