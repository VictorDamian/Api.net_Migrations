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

        public ClienteDAO(VentasContext context)
        {
            _context = context;
        }
        public async Task<List<ClienteDTO>> GetClienteAsync()
        {
            //var cliente = await _context.Clientes.ToListAsync();
            var model = await (from b in _context.Clientes
                        select new ClienteDTO()
                        {
                            Id = b.Id,
                            Nombre = b.Nombre
                        }).ToListAsync();
            return model;
        }
        public async Task<ClienteDTO> GetClienteById(int id)
        {

            var cliente = await _context.Clientes.FindAsync(id);
            if(cliente==null) return null;
            var lts = new ClienteDTO(){
                Id = cliente.Id,
                Nombre = cliente.Nombre
            };
            return lts;
        }

        public async Task<ClienteDTO> CreateCliente(ClienteDTO clienteDto)
        {
            var model = new Cliente();
            model.Id = clienteDto.Id;
            model.Nombre = clienteDto.Nombre;
            var result = await _context.Clientes.AddAsync(model);
            await _context.SaveChangesAsync();
            if (result == null) return null;
            return clienteDto;
        }
        public async Task<ClienteDTO> UpdateCliente(int id, ClienteDTO clienteDTO)
        {
            var cliente = _context.Clientes.FindAsync(id);
            try{
                if(id==clienteDTO.Id){
                    _context.Entry(cliente).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }catch(Exception){ throw; }
            return clienteDTO;
        }
        public async Task<int> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if(cliente==null)
                return 404;
            try{
                _context.Remove(cliente);
                await _context.SaveChangesAsync();
            }catch(Exception){throw;}
            return id;
        }
    }
}