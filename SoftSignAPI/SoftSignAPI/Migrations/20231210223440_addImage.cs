using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftSignAPI.Migrations
{
    /// <inheritdoc />
    public partial class addImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cc",
                table: "UserDocuments");

            migrationBuilder.AddColumn<byte[]>(
                name: "Paraphe",
                table: "UserDocuments",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Signature",
                table: "UserDocuments",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paraphe",
                table: "UserDocuments");

            migrationBuilder.DropColumn(
                name: "Signature",
                table: "UserDocuments");

            migrationBuilder.AddColumn<string>(
                name: "Cc",
                table: "UserDocuments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
