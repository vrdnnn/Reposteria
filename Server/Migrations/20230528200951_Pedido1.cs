using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Act17.Server.Migrations
{
    /// <inheritdoc />
    public partial class Pedido1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductoVenta");

            migrationBuilder.RenameColumn(
                name: "VentaId",
                table: "Ventas",
                newName: "PedidoId");

            migrationBuilder.CreateTable(
                name: "PedidoProducto",
                columns: table => new
                {
                    ProductosProductoId = table.Column<int>(type: "int", nullable: false),
                    VentasPedidoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoProducto", x => new { x.ProductosProductoId, x.VentasPedidoId });
                    table.ForeignKey(
                        name: "FK_PedidoProducto_Productos_ProductosProductoId",
                        column: x => x.ProductosProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoProducto_Ventas_VentasPedidoId",
                        column: x => x.VentasPedidoId,
                        principalTable: "Ventas",
                        principalColumn: "PedidoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoProducto_VentasPedidoId",
                table: "PedidoProducto",
                column: "VentasPedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoProducto");

            migrationBuilder.RenameColumn(
                name: "PedidoId",
                table: "Ventas",
                newName: "VentaId");

            migrationBuilder.CreateTable(
                name: "ProductoVenta",
                columns: table => new
                {
                    ProductosProductoId = table.Column<int>(type: "int", nullable: false),
                    VentasVentaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoVenta", x => new { x.ProductosProductoId, x.VentasVentaId });
                    table.ForeignKey(
                        name: "FK_ProductoVenta_Productos_ProductosProductoId",
                        column: x => x.ProductosProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoVenta_Ventas_VentasVentaId",
                        column: x => x.VentasVentaId,
                        principalTable: "Ventas",
                        principalColumn: "VentaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductoVenta_VentasVentaId",
                table: "ProductoVenta",
                column: "VentasVentaId");
        }
    }
}
