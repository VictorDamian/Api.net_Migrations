using ApiVentas.Models;
using ApiVentas.Models.DTOs;
using ApiVentas.Models.Response;
using ApiVentas.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController:ControllerBase
    {
        private readonly VentasContext _context;

        public ClienteController(VentasContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            DataResponse data = new DataResponse();
            try
            {
                var clientes = await _context.Clientes.ToListAsync();
                data.Data = 1;
                data.Data = clientes;
            }catch(Exception ex)
            {
                data.Messages = ex.Message;
            }
            return Ok(data);
        }
        [HttpPost]
        public async Task<ActionResult> PostClient(ClienteDTO clienteDTO)
        {
            DataResponse oData = new DataResponse();
            try
            {
                Cliente cliente = new Cliente();
                cliente.Nombre = clienteDTO.Nombre;
                   
                await _context.Clientes.AddAsync(cliente);
                await _context.SaveChangesAsync();
                oData.Success=1;
                oData.Data = cliente;
            }catch(Exception ex)
            {
                oData.Messages = ex.Message;
            }
            return Ok(oData);
        }
        [HttpPut]
        public async Task<ActionResult> PutCliente(ClienteDTO clienteDTO)
        {
            DataResponse oResposne = new DataResponse();
            Cliente clienteModel = await _context.Clientes.FindAsync(clienteDTO.Id);
            try
            {
                if(clienteModel==null){
                    return NotFound(oResposne);
                }

                clienteModel.Nombre = clienteDTO.Nombre;                
                _context.Entry(clienteModel).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                oResposne.Success=1;
                oResposne.Data = clienteModel;
                
            }catch(Exception ex)
            {
                oResposne.Messages = ex.Message;
            }
            return Ok(oResposne);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCliente(int id)
        {
            DataResponse oResponse = new DataResponse();
            try{
                Cliente clienteModel = await _context.Clientes.FindAsync(id);

                if(clienteModel==null){
                    return NotFound(oResponse);
                }
                _context.Remove(clienteModel);
                await _context.SaveChangesAsync();
                oResponse.Success = 1;
                oResponse.Data = clienteModel;
            }catch(Exception ex){
                oResponse.Messages = ex.Message;               
            }
            return Ok(oResponse);
        }
    }
}