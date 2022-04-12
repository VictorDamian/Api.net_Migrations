using System.Data;
using ApiVentas.Models.DTOs;
using ApiVentas.Models.Response;
using ApiVentas.Services;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ApiVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController:ControllerBase
    {
        private readonly string stringConnection;
        private readonly IUsuarioService _service;
        private string query;

        public UsuariosController(IUsuarioService service)
        {
            _service = service;
            stringConnection = "data source =dantes; initial catalog = VentasReal; integrated security = true;";
            query = "insert into usuarios values(@username, @Email, @Password)";
        }
        [HttpGet]
        public async Task<ActionResult> GetAll(){
            string query = "select username, email from usuarios";
            using (var db = new SqlConnection(stringConnection)){
                var param = new DynamicParameters();
                var o = await db.QueryAsync<UsuarioDTO>(query);
                return Ok(o);
            }
        }
        [HttpPost]
        public async Task<ActionResult> PostUsuario(UsuarioDTO dto)
        {
            var param = new DynamicParameters();
            param.Add("Username", dto.Username, DbType.String);
            param.Add("Email", dto.Email, DbType.String);
            param.Add("password", dto.Password, DbType.String);
            
            using (var conn = new SqlConnection(stringConnection)){
                await conn.ExecuteAsync(query, param);
            }
            return Ok(dto);
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(AuthResponse model)
        {
            DataResponse resp = new DataResponse();
            var usr =  _service.Authenticate(model);
            if(usr==null){
                resp.Messages = "Error al autenticar usuario";
                return BadRequest(resp);
            }
            resp.Success = 1;
            resp.Data = usr;
            return Ok(resp);
        }
    }
}