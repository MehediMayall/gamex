using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gamex.Auth.Services.Migrations
{
    /// <inheritdoc />
    public partial class added_lastusedpassword_in_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "last_used_password",
                table: "users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_used_password",
                table: "users");
        }
    }
}
