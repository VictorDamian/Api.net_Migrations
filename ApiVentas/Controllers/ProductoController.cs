using ApiVentas.Models;
using ApiVentas.Models.DTOs;
using ApiVentas.Models.Response;
using ApiVentas.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult> Add(VentaDTO dto)
        {
            DataResponse oResp = new DataResponse();
            try
            {
                using(var transaction = _context.Database.BeginTransaction())
                {
                    try{
                        var venta = new Venta();
                        venta.Fecha = DateTime.Now.ToString();
                        venta.ClienteId = dto.ClienteId;
                        venta.Total = dto.DetalleVentas.Sum(s=>s.Cantidad*s.PrecioU);
                        await _context.Ventas.AddAsync(venta);
                        await _context.SaveChangesAsync();

                        foreach(var c in dto.DetalleVentas){
                            var entity = new Models.DetalleVenta();
                            entity.VentaId = venta.Id;
                            entity.ProductoId = c.ProductoId;
                            entity.PrecioU = c.PrecioU;
                            entity.Cantidad = c.Cantidad;
                            entity.Importe = c.Importe;
                            await _context.DetalleVentas.AddAsync(entity);
                            await _context.SaveChangesAsync();
                        }
                        transaction.Commit();
                        oResp.Success = 1;
                    }catch(Exception){
                        transaction.Rollback();
                    }
                    
                }
                
            }
            catch(Exception ex){
                oResp.Messages = ex.Message;
            }
            return Ok(oResp);
        }
    }
}