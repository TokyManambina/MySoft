using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftSignAPI.Migrations
{
    /// <inheritdoc />
    public partial class tablechange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentDetailItems_DocumentDetails_DetailId",
                table: "DocumentDetailItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentDetails_Subscriptions_SubscriptionId",
                table: "DocumentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentDynamicFields_DocumentDetails_DocumentDetailId",
                table: "DocumentDynamicFields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentDetails",
                table: "DocumentDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentDetailItems",
                table: "DocumentDetailItems");

            migrationBuilder.RenameTable(
                name: "DocumentDetails",
                newName: "DynamicFields");

            migrationBuilder.RenameTable(
                name: "DocumentDetailItems",
                newName: "DynamicFieldItems");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentDetails_SubscriptionId",
                table: "DynamicFields",
                newName: "IX_DynamicFields_SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentDetailItems_DetailId",
                table: "DynamicFieldItems",
                newName: "IX_DynamicFieldItems_DetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DynamicFields",
                table: "DynamicFields",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DynamicFieldItems",
                table: "DynamicFieldItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentDynamicFields_DynamicFields_DocumentDetailId",
                table: "DocumentDynamicFields",
                column: "DocumentDetailId",
                principalTable: "DynamicFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicFieldItems_DynamicFields_DetailId",
                table: "DynamicFieldItems",
                column: "DetailId",
                principalTable: "DynamicFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicFields_Subscriptions_SubscriptionId",
                table: "DynamicFields",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentDynamicFields_DynamicFields_DocumentDetailId",
                table: "DocumentDynamicFields");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicFieldItems_DynamicFields_DetailId",
                table: "DynamicFieldItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicFields_Subscriptions_SubscriptionId",
                table: "DynamicFields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DynamicFields",
                table: "DynamicFields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DynamicFieldItems",
                table: "DynamicFieldItems");

            migrationBuilder.RenameTable(
                name: "DynamicFields",
                newName: "DocumentDetails");

            migrationBuilder.RenameTable(
                name: "DynamicFieldItems",
                newName: "DocumentDetailItems");

            migrationBuilder.RenameIndex(
                name: "IX_DynamicFields_SubscriptionId",
                table: "DocumentDetails",
                newName: "IX_DocumentDetails_SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_DynamicFieldItems_DetailId",
                table: "DocumentDetailItems",
                newName: "IX_DocumentDetailItems_DetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentDetails",
                table: "DocumentDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentDetailItems",
                table: "DocumentDetailItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentDetailItems_DocumentDetails_DetailId",
                table: "DocumentDetailItems",
                column: "DetailId",
                principalTable: "DocumentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentDetails_Subscriptions_SubscriptionId",
                table: "DocumentDetails",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentDynamicFields_DocumentDetails_DocumentDetailId",
                table: "DocumentDynamicFields",
                column: "DocumentDetailId",
                principalTable: "DocumentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
