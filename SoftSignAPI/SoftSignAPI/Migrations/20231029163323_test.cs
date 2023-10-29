using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftSignAPI.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Societies_SocietyId",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "SocietyId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Storage",
                table: "Societies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Societies_SocietyId",
                table: "Users",
                column: "SocietyId",
                principalTable: "Societies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Societies_SocietyId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Storage",
                table: "Societies");

            migrationBuilder.AlterColumn<Guid>(
                name: "SocietyId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Societies_SocietyId",
                table: "Users",
                column: "SocietyId",
                principalTable: "Societies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
