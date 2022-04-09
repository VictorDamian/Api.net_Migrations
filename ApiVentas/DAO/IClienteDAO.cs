using ApiVentas.Models;
using ApiVentas.Models.DTOs;

namespace ApiVentas.DAO
{
    public interface IClienteDAO
    {
        Task<List<ClienteDTO>> GetClienteAsync();
        Task<ClienteDTO> GetClienteById(int id);
        Task<ClienteDTO> CreateCliente(ClienteDTO clienteDTO);
        Task UpdateCliente(int id, Cliente cliente);
        Task<Cliente> DeleteClinete(int id);
        
    }
}