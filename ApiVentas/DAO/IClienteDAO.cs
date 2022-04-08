using ApiVentas.Models;
using ApiVentas.Models.DTOs;

namespace ApiVentas.DAO
{
    public interface IClienteDAO
    {
        Task<List<Cliente>> GetClienteAsync();
        Task CreateCliente(Cliente cliente);
        Task UpdateCliente(int id, Cliente cliente);
        Task DeleteClinete(int id);
        Task<Cliente> GetClienteById(int id);
    }
}