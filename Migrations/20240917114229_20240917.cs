using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftsStore.Migrations
{
    /// <inheritdoc />
    public partial class _20240917 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RateGiftUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateGiftUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RateGiftUsers_AspNetUsers_PersonId",
                        column: x => x.PersonId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RateGiftUsers_Gifts_GiftId",
                        column: x => x.GiftId,
                        principalTable: "Gifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RateStoreUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateStoreUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RateStoreUsers_AspNetUsers_PersonId",
                        column: x => x.PersonId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RateStoreUsers_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RateGiftUsers_GiftId",
                table: "RateGiftUsers",
                column: "GiftId");

            migrationBuilder.CreateIndex(
                name: "IX_RateGiftUsers_PersonId",
                table: "RateGiftUsers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_RateStoreUsers_PersonId",
                table: "RateStoreUsers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_RateStoreUsers_StoreId",
                table: "RateStoreUsers",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RateGiftUsers");

            migrationBuilder.DropTable(
                name: "RateStoreUsers");
        }
    }
}
