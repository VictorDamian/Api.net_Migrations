using ApiVentas.Models;
using ApiVentas.Models.DTOs;
using ApiVentas.Models.Response;
using ApiVentas.Repositories;
using ApiVentas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductoController:ControllerBase
    {
        private readonly IVentaService _iventaService;

        public ProductoController(IVentaService iventaService)
        {
            _iventaService = iventaService;
        }
        [HttpPost]
        public async Task<ActionResult> Add(VentaDTO dto)
        {
            DataResponse oResp = new DataResponse();
            try
            {
                await _iventaService.Add(dto);
                oResp.Success = 1;
            }
            catch(Exception ex){
                oResp.Messages = ex.Message;
            }
            return Ok(oResp);
        }
    }
}