using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsAPI.Migrations
{
    /// <inheritdoc />
    public partial class PopulateCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Name", "ImageUrl" },
                values: new object[] { "Drinks", "drinks.jpg" });
            
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Name", "ImageUrl" },
                values: new object[] { "Snacks", "snacks.jpg" });
            
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Name", "ImageUrl" },
                values: new object[] { "Desserts", "desserts.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categories;");
        }
    }
}
