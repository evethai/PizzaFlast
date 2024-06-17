using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerDrinks_CustomerOrders_CustomerOrderOrderId",
                table: "CustomerDrinks");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPizzas_CustomerOrders_CustomerOrderOrderId",
                table: "CustomerPizzas");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPizzas_CustomerOrderOrderId",
                table: "CustomerPizzas");

            migrationBuilder.DropIndex(
                name: "IX_CustomerDrinks_CustomerOrderOrderId",
                table: "CustomerDrinks");

            migrationBuilder.DropColumn(
                name: "CustomerOrderOrderId",
                table: "CustomerPizzas");

            migrationBuilder.DropColumn(
                name: "CustomerOrderOrderId",
                table: "CustomerDrinks");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPizzas_OrderId",
                table: "CustomerPizzas",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDrinks_OrderId",
                table: "CustomerDrinks",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerDrinks_CustomerOrders_OrderId",
                table: "CustomerDrinks",
                column: "OrderId",
                principalTable: "CustomerOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPizzas_CustomerOrders_OrderId",
                table: "CustomerPizzas",
                column: "OrderId",
                principalTable: "CustomerOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerDrinks_CustomerOrders_OrderId",
                table: "CustomerDrinks");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPizzas_CustomerOrders_OrderId",
                table: "CustomerPizzas");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPizzas_OrderId",
                table: "CustomerPizzas");

            migrationBuilder.DropIndex(
                name: "IX_CustomerDrinks_OrderId",
                table: "CustomerDrinks");

            migrationBuilder.AddColumn<int>(
                name: "CustomerOrderOrderId",
                table: "CustomerPizzas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerOrderOrderId",
                table: "CustomerDrinks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPizzas_CustomerOrderOrderId",
                table: "CustomerPizzas",
                column: "CustomerOrderOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDrinks_CustomerOrderOrderId",
                table: "CustomerDrinks",
                column: "CustomerOrderOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerDrinks_CustomerOrders_CustomerOrderOrderId",
                table: "CustomerDrinks",
                column: "CustomerOrderOrderId",
                principalTable: "CustomerOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPizzas_CustomerOrders_CustomerOrderOrderId",
                table: "CustomerPizzas",
                column: "CustomerOrderOrderId",
                principalTable: "CustomerOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
