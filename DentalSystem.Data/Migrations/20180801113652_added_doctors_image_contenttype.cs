using Microsoft.EntityFrameworkCore.Migrations;

namespace DentalSystem.Data.Migrations
{
    public partial class added_doctors_image_contenttype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageContentType",
                table: "Doctors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageContentType",
                table: "Doctors");
        }
    }
}
