using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Novin.Warehouse.Backend.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLocationFromInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Inventories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
