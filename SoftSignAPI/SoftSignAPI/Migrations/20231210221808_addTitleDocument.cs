using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftSignAPI.Migrations
{
    /// <inheritdoc />
    public partial class addTitleDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Documents");
        }
    }
}
