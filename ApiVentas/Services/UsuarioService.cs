using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiVentas.Common;
using ApiVentas.DAO;
using ApiVentas.Models;
using ApiVentas.Models.Response;
using ApiVentas.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ApiVentas.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly VentasContext _db;
        private readonly JwtSetting _jwtSetting;
        public UsuarioService(IOptions<JwtSetting> jwtSetting, VentasContext db)
        {
            this._jwtSetting = jwtSetting.Value;
            _db = db;
        }
        public UsuarioResponse Authenticate(AuthResponse resp)
        {
            UsuarioResponse response = new UsuarioResponse();

            var usr = _db.Usuarios.Where(
                u=>u.Username == resp.Username && 
                u.Password == resp.Password).FirstOrDefault();
                
            if(usr == null)
                return null;
                
            response.Username = usr.Username;
            response.Token = GetToken(usr);
            return response;
        }
        private string GetToken(Usuario usr)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSetting.Code);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usr.Id.ToString()),
                        new Claim(ClaimTypes.Email, usr.Email.ToString())
                    }),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}