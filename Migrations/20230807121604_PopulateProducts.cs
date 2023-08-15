using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogAPI.Migrations
{
    /// <inheritdoc />
    public partial class PopulateProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Name", "Description", "Price", "ImageUrl", "Stock", "RegisterDate", "CategoryId" },
                values: new object[] { "Diet Coke", "Cola Soda 350ml", 5.45, "coke.jpg", 50, DateTime.Now.ToUniversalTime(), 1 });
            
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Name", "Description", "Price", "ImageUrl", "Stock", "RegisterDate", "CategoryId" },
                values: new object[] { "Tuna Snack", "Tuna sandwich with mayo", 8.50, "tuna.jpg", 10, DateTime.Now.ToUniversalTime(), 2 });
            
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Name", "Description", "Price", "ImageUrl", "Stock", "RegisterDate", "CategoryId" },
                values: new object[] { "Pudim", "Pudim 100g", 6.75, "pudim.jpg", 20, DateTime.Now.ToUniversalTime(), 3 });
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Products;");
        }
    }
}
