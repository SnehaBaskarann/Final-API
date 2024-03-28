using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobileStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class Arun : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MobileId",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_orders_MobileId",
                table: "orders",
                column: "MobileId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_mobiles_MobileId",
                table: "orders",
                column: "MobileId",
                principalTable: "mobiles",
                principalColumn: "MobileId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_mobiles_MobileId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_MobileId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "MobileId",
                table: "orders");
        }
    }
}
