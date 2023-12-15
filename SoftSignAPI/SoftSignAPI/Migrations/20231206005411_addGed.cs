using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftSignAPI.Migrations
{
    /// <inheritdoc />
    public partial class addGed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Offers_OfferId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_OfferId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Cc",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "DateUpdate",
                table: "Subscriptions",
                newName: "UpdatedDate");

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasClientSpace",
                table: "Subscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDynamicFieldManager",
                table: "Subscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasFlow",
                table: "Subscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasFlowManager",
                table: "Subscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasLibrary",
                table: "Subscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasPhysicalLibrary",
                table: "Subscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxUser",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Attachements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    Filename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachements_Documents_DocumentCode",
                        column: x => x.DocumentCode,
                        principalTable: "Documents",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isRequired = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentDetails_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentLinks",
                columns: table => new
                {
                    CodeLink = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodeDocument = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentLinks", x => x.CodeLink);
                    table.ForeignKey(
                        name: "FK_DocumentLinks_Documents_CodeDocument",
                        column: x => x.CodeDocument,
                        principalTable: "Documents",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Flows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flows_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Historiques",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Action = table.Column<int>(type: "int", nullable: false),
                    Table = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Colonne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historiques", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentDetailItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentDetailItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentDetailItems_DocumentDetails_DetailId",
                        column: x => x.DetailId,
                        principalTable: "DocumentDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentDynamicFields",
                columns: table => new
                {
                    DocumentCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DocumentDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentDynamicFields", x => new { x.DocumentCode, x.DocumentDetailId });
                    table.ForeignKey(
                        name: "FK_DocumentDynamicFields_DocumentDetails_DocumentDetailId",
                        column: x => x.DocumentDetailId,
                        principalTable: "DocumentDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentDynamicFields_Documents_DocumentCode",
                        column: x => x.DocumentCode,
                        principalTable: "Documents",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFlows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFlows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFlows_Flows_FlowId",
                        column: x => x.FlowId,
                        principalTable: "Flows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachements_DocumentCode",
                table: "Attachements",
                column: "DocumentCode");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDetailItems_DetailId",
                table: "DocumentDetailItems",
                column: "DetailId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDetails_SubscriptionId",
                table: "DocumentDetails",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDynamicFields_DocumentDetailId",
                table: "DocumentDynamicFields",
                column: "DocumentDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentLinks_CodeDocument",
                table: "DocumentLinks",
                column: "CodeDocument");

            migrationBuilder.CreateIndex(
                name: "IX_Flows_SubscriptionId",
                table: "Flows",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFlows_FlowId",
                table: "UserFlows",
                column: "FlowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachements");

            migrationBuilder.DropTable(
                name: "DocumentDetailItems");

            migrationBuilder.DropTable(
                name: "DocumentDynamicFields");

            migrationBuilder.DropTable(
                name: "DocumentLinks");

            migrationBuilder.DropTable(
                name: "Historiques");

            migrationBuilder.DropTable(
                name: "UserFlows");

            migrationBuilder.DropTable(
                name: "DocumentDetails");

            migrationBuilder.DropTable(
                name: "Flows");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "HasClientSpace",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "HasDynamicFieldManager",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "HasFlow",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "HasFlowManager",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "HasLibrary",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "HasPhysicalLibrary",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "MaxUser",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Subscriptions",
                newName: "DateUpdate");

            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                table: "Subscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cc",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_OfferId",
                table: "Subscriptions",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Offers_OfferId",
                table: "Subscriptions",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id");
        }
    }
}
