using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace BookSharingOnlineApi.Migrations
{
    public partial class BookMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookAuthorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookPrice = table.Column<double>(type: "float", nullable: false),
                    BookRating = table.Column<double>(type: "float", nullable: false),
                    BookNumberOfRatings = table.Column<int>(type: "int", nullable: false),
                    BookQuantity = table.Column<int>(type: "int", nullable: false),
                    BookQuantitySold = table.Column<int>(type: "int", nullable: false),
                    BookAddTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookCoverPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}