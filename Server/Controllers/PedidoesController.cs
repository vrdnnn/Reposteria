using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Act17.Server;
using Act17.Shared.Models;
using Act17.Server.Migrations;

namespace Act17.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoesController : ControllerBase
    {
        private readonly TiendaContexto _context;

        public PedidoesController(TiendaContexto context)
        {
            _context = context;
        }

        // GET: api/Pedidoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetVentas()
        {
          if (_context.Ventas == null)
          {
              return NotFound();
          }

            return await _context.Ventas
                     .Include(v => v.Cliente)
                     .Include(v => v.Productos)
                     .ToListAsync();
        }

        // GET: api/Pedidoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
          if (_context.Ventas == null)
          {
              return NotFound();
          }
            var pedido = await _context.Ventas.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        // PUT: api/Pedidoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.PedidoId)
            {
                return BadRequest();
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pedidoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {

            if (pedido.ProductoIds != null && pedido.ProductoIds.Any())
            {
                pedido.Productos = new List<Producto>();

                foreach (var productoId in pedido.ProductoIds)
                {
                    var producto = await _context.Productos.FindAsync(productoId);
                    if (producto != null)
                    {
                        producto.Stock -= pedido.Cantidad;
                        pedido.Productos.Add(producto);
                    }
                }
            }

            _context.Ventas.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedido", new { id = pedido.PedidoId }, pedido);
        }



        // DELETE: api/Pedidoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            if (_context.Ventas == null)
            {
                return NotFound();
            }
            var pedido = await _context.Ventas.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.Ventas.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(int id)
        {
            return (_context.Ventas?.Any(e => e.PedidoId == id)).GetValueOrDefault();
        }
    }
}
