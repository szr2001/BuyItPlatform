using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuyItPlatform.CommentsApi.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 200, nullable: false, defaultValueSql: "NOW()"),
                    Content = table.Column<string>(type: "text", nullable: false),
                    ListingId = table.Column<string>(type: "character varying(38)", maxLength: 38, nullable: false),
                    UserId = table.Column<string>(type: "character varying(38)", maxLength: 38, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");
        }
    }
}
