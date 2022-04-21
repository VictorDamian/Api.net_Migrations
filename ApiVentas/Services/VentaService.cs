using ApiVentas.Models;
using ApiVentas.Models.DTOs;
using ApiVentas.Repositories;

namespace ApiVentas.Services
{
    public class VentaService : IVentaService
    {
        private readonly VentasContext _context;
        
        public VentaService(VentasContext context)
        {
            _context = context;
        }
        public async Task Add(VentaDTO dto)
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
                }catch(Exception){
                    transaction.Rollback();
                    throw new Exception("Fail to insert");
                }
            }
        }
    }
}