using System;
using System.Linq;
using System.Threading.Tasks;
using ApiVentas;
using ApiVentas.Controllers;
using ApiVentas.DAO;
using ApiVentas.Models;
using ApiVentas.Models.DTO;
using ApiVentas.Models.DTOs;
using ApiVentas.Repositories;
using AutoMapper;
using Xunit;
using xUnitVentasRest.DataContext;

namespace xUnitVentasRest.Pruebas
{
    public class ProductosTest:IDisposable
    {
        private VentasContext _context;
        private static IMapper _mapper;
        public ProductosTest()
        {
            _context = new VentasContextMemory().GetContext();

            if(_mapper==null){
                var mappingCfg = new MapperConfiguration(mc=>{
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingCfg.CreateMapper();
                _mapper = mapper;
            }
        }
        
        [Fact]
        public async Task Get_returnAllResult_trueAsync()
        {
            var p = new ClienteDAO(_context);
            await p.Create(new Cliente
            {
                Nombre = "Jimmy"
            });
            await p.Save();
            var s = p.GetAll().Result.Count();
            Assert.Equal(3, s);
        }
        [Fact]
        public async Task Get_ShouldReturnClients_falseAsync()
        {
            var p = new ClienteDAO(_context);
            var clienteDto = new ClienteCreateDto()
            {
                Nombre = "Jimmy"
            };
            var model = _mapper.Map<Cliente>(clienteDto);
            await p.Create(model);
            await p.Save();
            var s = p.GetAll().Result.Count();
            Assert.NotEqual(2, s);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}