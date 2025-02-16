using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuyItPlatform.ListingsApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedNullablePropertiesDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int[]>(
                name: "Tags",
                table: "Listings",
                type: "integer[]",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(int[]),
                oldType: "integer[]",
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<int>(
                name: "SubCategory",
                table: "Listings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string[]>(
                name: "ImagePaths",
                table: "Listings",
                type: "text[]",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string[]),
                oldType: "text[]",
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<int>(
                name: "Color",
                table: "Listings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int[]>(
                name: "Tags",
                table: "Listings",
                type: "integer[]",
                maxLength: 5,
                nullable: false,
                defaultValue: new int[0],
                oldClrType: typeof(int[]),
                oldType: "integer[]",
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubCategory",
                table: "Listings",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string[]>(
                name: "ImagePaths",
                table: "Listings",
                type: "text[]",
                maxLength: 3,
                nullable: false,
                defaultValue: new string[0],
                oldClrType: typeof(string[]),
                oldType: "text[]",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Color",
                table: "Listings",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
