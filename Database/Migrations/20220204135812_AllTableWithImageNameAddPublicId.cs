using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class AllTableWithImageNameAddPublicId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "CitiesPlaces",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "Cities",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "CategoriesCities",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarPublicId",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "CitiesPlaces");

            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "CategoriesCities");

            migrationBuilder.DropColumn(
                name: "AvatarPublicId",
                table: "AspNetUsers");
        }
    }
}
