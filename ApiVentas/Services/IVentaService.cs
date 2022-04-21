using ApiVentas.Models.DTOs;

namespace ApiVentas.Services
{
    public interface IVentaService
    {
        Task Add(VentaDTO dto);
    }
}