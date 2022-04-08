using ApiVentas.Models;
using ApiVentas.Models.DTOs;
using ApiVentas.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiVentas.DAO
{
    public class ClienteDAO:IClienteDAO
    {
        private readonly VentasContext _context;
        private readonly IMapper _mapper;

        public ClienteDAO(VentasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<Cliente>> GetClienteAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task CreateCliente(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCliente(int id, Cliente cliente)
        {
            try{
                if(id==cliente.Id)
                    _context.Entry(cliente).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
            }catch(Exception){ throw; }
        }
        public async Task DeleteClinete(int id)
        {
            try{
                var clienteModel = await _context.Clientes.FindAsync(id);
                if(clienteModel!=null){
                    _context.Remove(clienteModel);
                    await _context.SaveChangesAsync();    
                }
            }catch(Exception){throw;}
        }

        public async Task<Cliente> GetClienteById(int id)
        {
            var cli =  await _context.Clientes.FindAsync(id);
            if(cli==null)
                return null;
            return cli;
        }
    }
}