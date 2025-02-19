using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftsStore.Migrations
{
    /// <inheritdoc />
    public partial class _20250213 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Gifts_GiftId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "GiftId",
                table: "Comments",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_GiftId",
                table: "Comments",
                newName: "IX_Comments_StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Stores_StoreId",
                table: "Comments",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Stores_StoreId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "Comments",
                newName: "GiftId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_StoreId",
                table: "Comments",
                newName: "IX_Comments_GiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Gifts_GiftId",
                table: "Comments",
                column: "GiftId",
                principalTable: "Gifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
