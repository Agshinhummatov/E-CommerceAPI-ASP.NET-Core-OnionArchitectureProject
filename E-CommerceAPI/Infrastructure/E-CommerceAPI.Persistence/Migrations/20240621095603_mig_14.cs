using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceAPI.Persistence.Migrations
{
    public partial class mig_14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedOrders_Orders_Id",
                table: "CompletedOrders");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedOrders_OrderId",
                table: "CompletedOrders",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedOrders_Orders_OrderId",
                table: "CompletedOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedOrders_Orders_OrderId",
                table: "CompletedOrders");

            migrationBuilder.DropIndex(
                name: "IX_CompletedOrders_OrderId",
                table: "CompletedOrders");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedOrders_Orders_Id",
                table: "CompletedOrders",
                column: "Id",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
