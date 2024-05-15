using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Craftify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class user_modification_added_emailConfimation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Users");
        }
    }
}
