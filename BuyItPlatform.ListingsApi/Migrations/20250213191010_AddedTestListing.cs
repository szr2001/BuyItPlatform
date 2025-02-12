using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuyItPlatform.ListingsApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedTestListing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Listings",
                columns: new[] { "Id", "Category", "Color", "Currency", "Description", "ListingType", "Name", "Price", "SubCategory", "Tags", "UserId" },
                values: new object[] { 1, 3, 10, "Euro", "A good chair", 0, "Chair", 10f, 0, new[] { 8, 31 }, 123 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
