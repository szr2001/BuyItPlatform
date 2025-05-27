using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuyItPlatform.ListingsApi.Migrations
{
    /// <inheritdoc />
    public partial class addedSlotId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SlotId",
                table: "Listings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlotId",
                table: "Listings");
        }
    }
}
