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
            await _context.SaveChangesAsync();
            if(cliente==null) return null;
            return clienteDto;
        }
        public async Task<ClienteDTO> UpdateCliente(int id, ClienteDTO clienteDTO)
        {
            try{
                if(id==clienteDTO.Id){
                    var o = _mapper.Map<Cliente>(clienteDTO);
                    _context.Entry(o).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }catch(Exception){ throw; }
            return clienteDTO;
        }
        public async Task<int> DeleteCliente(int id)
        {
            var clienteId = await _context.Clientes.FindAsync(id);
            if(clienteId==null)
                return 404;
            var cliente = _mapper.Map<Cliente>(clienteId);
            try{
                _context.Remove(cliente);
                await _context.SaveChangesAsync();
            }catch(Exception){throw;}
            return id;
        }
    }
}