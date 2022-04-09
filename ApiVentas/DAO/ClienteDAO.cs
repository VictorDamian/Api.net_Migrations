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
        public async Task<List<ClienteDTO>> GetClienteAsync()
        {
            var cliente = await _context.Clientes.ToListAsync();
            var mapperCliente = _mapper.Map<List<ClienteDTO>>(cliente);
            return mapperCliente;
        }
        public async Task<ClienteDTO> GetClienteById(int id)
        {
            var clienteId = await _context.Clientes.FindAsync(id);
            if(clienteId==null) return null;
            var mapper = _mapper.Map<ClienteDTO>(clienteId);
            return mapper;
        }

        public async Task<ClienteDTO> CreateCliente(ClienteDTO clienteDto)
        {
            //Mapper
            var cliente = _mapper.Map<Cliente>(clienteDto);
            var result = await _context.Clientes.AddAsync(cliente);
            var m = _mapper.Map<ClienteDTO>(result);
            await _context.SaveChangesAsync();
            if(result==null) return null;
            return m;
        }
        public async Task UpdateCliente(int id, Cliente cliente)
        {
            try{
                if(id==cliente.Id)
                    _context.Entry(cliente).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
            }catch(Exception){ throw; }
        }
        public async Task<Cliente> DeleteClinete(int id)
        {
            var clienteId = await _context.Clientes.FindAsync(id);
            if(clienteId==null)
                return null;
            Cliente cliente = _mapper.Map<Cliente>(clienteId);
            try{
                _context.Remove(cliente);
                await _context.SaveChangesAsync();
            }catch(Exception){throw;}
            return cliente;
        }

        
    }
}