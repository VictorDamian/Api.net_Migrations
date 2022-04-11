using ApiVentas.Models;
using ApiVentas.Models.DTOs;

namespace ApiVentas.DAO
{
    public interface IClienteDAO
    {
        Task<List<ClienteDTO>> GetClienteAsync();
        Task<ClienteDTO> GetClienteById(int id);
        Task<ClienteDTO> CreateCliente(ClienteDTO clienteDTO);
        Task<ClienteDTO> UpdateCliente(int id, ClienteDTO clienteDTO);
        Task<int> DeleteCliente(int id);

    }
}