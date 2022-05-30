using System.Linq;
using System.Threading.Tasks;
using ApiVentas.Controllers;
using ApiVentas.DAO;
using ApiVentas.Models;
using ApiVentas.Models.DTOs;
using ApiVentas.Repositories;
using AutoMapper;
using Xunit;
using xUnitVentasRest.DataContext;

namespace xUnitVentasRest.Pruebas
{
    public class ProductosTest
    {
        private VentasContext _context;
        public ProductosTest()
        {
            _context = new VentasContextMemory().GetContext();
        }
        
        //[Fact]
        public async Task Get_returnAllResult_trueAsync()
        {
            var p = new ClienteDAO(_context);
            var c = new ClienteDTO()
            {
                Id = 1,
                Nombre = "Jimmy"
            };
            await p.CreateCliente(c);
            var s = p.GetClienteAsync().Result.Count;
            Assert.Equal(3, s);
        }
        [Fact]
        public async Task Get_ShouldReturnClients_falseAsync()
        {
            var p = new ClienteDAO(_context);
            var c = new ClienteDTO()
            {
                Id = 1,
                Nombre = "Jimmy"
            };
            await p.CreateCliente(c);
            var s = p.GetClienteAsync().Result.Count;
            Assert.NotEqual(2, s);
        }
    }
}