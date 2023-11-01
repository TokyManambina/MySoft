using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftSignAPI.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cc",
                table: "UserDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "UserDocuments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cc",
                table: "UserDocuments");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "UserDocuments");
        }
    }
}
