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
        public IEnumerable<Cliente> GetListByName(string param)
        {
            return _context.Clientes.Where(x=>x.Nombre == param).ToList();
        }

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            return await _context.Clientes.ToListAsync();
        }

        public Cliente GetById(int idPk) => _context.Clientes.FirstOrDefault(x => x.Id == idPk);

        public async Task Create(Cliente entity)
        {
            if(entity==null)
                throw new ArgumentNullException(nameof(entity));
            await _context.Clientes.AddAsync(entity);
        }

        public async Task Update(Cliente entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await Save();
        }

        public async Task Delete(Cliente entity)
        {
            if(entity==null)
                throw new ArgumentNullException(nameof(entity));
            _context.Clientes.Remove(entity);
            await Save();
        }

        public async Task<bool> Save()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}