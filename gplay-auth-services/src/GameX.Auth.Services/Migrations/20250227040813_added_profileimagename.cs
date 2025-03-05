using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gamex.Auth.Services.Migrations
{
    /// <inheritdoc />
    public partial class added_profileimagename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "profile_imagename",
                table: "players",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profile_imagename",
                table: "players");
        }
    }
}
