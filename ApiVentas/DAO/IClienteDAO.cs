using ApiVentas.Models;
using ApiVentas.Models.DTOs;

namespace ApiVentas.DAO
{
    public interface IClienteDAO:IGenericRepository<Cliente>
    {
        IEnumerable<Cliente> GetListByName(String param);
    }
}