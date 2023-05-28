using Act17.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Act17.Server
{
    public class TiendaContexto: DbContext
    {
        public TiendaContexto(DbContextOptions<TiendaContexto> options) : base(options)
        {

        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Ventas { get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}
