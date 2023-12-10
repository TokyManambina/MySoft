using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftSignAPI.Migrations
{
    /// <inheritdoc />
    public partial class changepfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Page",
                table: "Fields",
                newName: "LastPage");

            migrationBuilder.AddColumn<string>(
                name: "FirstPage",
                table: "Fields",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstPage",
                table: "Fields");

            migrationBuilder.RenameColumn(
                name: "LastPage",
                table: "Fields",
                newName: "Page");
        }
    }
}
