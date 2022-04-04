
//using ApiVentas.Models;
using ApiVentas.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiVentas.Repositories
{
    public class VentasContext : DbContext
    {
        public VentasContext(DbContextOptions<VentasContext> options) : base(options)
        {
        }
        public virtual DbSet<Cliente> Clientes {get;set;}
        public virtual DbSet<Venta> Ventas {get;set;}

        public virtual DbSet<Usuario> Usuarios {get;set;}
        public virtual DbSet<Producto> Productos {get;set;}
        public virtual DbSet<DetalleVenta> DetalleVentas{get;set;}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //poblar bd
        }
    }
}