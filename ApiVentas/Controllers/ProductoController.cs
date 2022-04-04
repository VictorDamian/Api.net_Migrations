using ApiVentas.Models;
using ApiVentas.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController:ControllerBase
    {
        private readonly VentasContext _context;

        public ProductoController(VentasContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var venta_cliente = await (from venta in _context.Ventas
             join cliente in _context.Clientes
                on venta.ClienteId equals cliente.Id
                select new
                {
                    venta.Id,
                    venta.Fecha,
                    IdCliente = cliente.Id,
                    NombreCliente = cliente.Nombre
                }).ToListAsync();
            return Ok(venta_cliente);
        }
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            _context.Add(cliente);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}