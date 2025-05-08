using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "restaurants",
                columns: table => new
                {
                    Restaurant_ID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Zip_Code = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Alcohol_Service = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Smoking_Allowed = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Price = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Franchise = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Area = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Parking = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_restaurants", x => x.Restaurant_ID);
                });

            migrationBuilder.CreateTable(
                name: "ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Consumer_ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Restaurant_ID = table.Column<int>(type: "int", nullable: false),
                    Overall_Rating = table.Column<byte>(type: "tinyint", nullable: false),
                    Food_Rating = table.Column<byte>(type: "tinyint", nullable: false),
                    Service_Rating = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ratings__3214EC077D0525B0", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ratings_restaurants",
                        column: x => x.Restaurant_ID,
                        principalTable: "restaurants",
                        principalColumn: "Restaurant_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ratings_Restaurant_ID",
                table: "ratings",
                column: "Restaurant_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ratings");

            migrationBuilder.DropTable(
                name: "restaurants");
        }
    }
}
