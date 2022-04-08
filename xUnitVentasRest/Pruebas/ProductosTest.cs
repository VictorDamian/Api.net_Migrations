using System.Threading.Tasks;
using ApiVentas.Controllers;
using ApiVentas.Models;
using ApiVentas.Repositories;
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
        
        [Fact]
        public async Task Get_returnAllResult_trueAsync()
        {
            var p = new ProductoController(_context);
            await p.PostCliente(new Cliente());
            var s = await p.GetAll();
            Assert.Equal(1, s);
        }
    }
}