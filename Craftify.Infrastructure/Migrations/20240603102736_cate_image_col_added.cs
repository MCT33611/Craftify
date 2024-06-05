using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Craftify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cate_image_col_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Categories");
        }
    }
}
