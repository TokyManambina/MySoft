using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftSignAPI.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "UserDocuments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "MyTurn",
                table: "UserDocuments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "UserDocuments");

            migrationBuilder.DropColumn(
                name: "MyTurn",
                table: "UserDocuments");
        }
    }
}
