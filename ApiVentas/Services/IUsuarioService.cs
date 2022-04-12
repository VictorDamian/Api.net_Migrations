using ApiVentas.Models.Response;

namespace ApiVentas.Services
{
    public interface IUsuarioService
    {
         UsuarioResponse Authenticate(AuthResponse resp);
    }
}