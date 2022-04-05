using ApiVentas.Models;
using ApiVentas.Models.DTOs;
using ApiVentas.Models.Response;
using ApiVentas.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController:ControllerBase
    {
        private readonly VentasContext _context;
        private readonly IMapper _mapper;
        public ClienteController(VentasContext context, IMapper mapper)
        {
            _context=context;
            _mapper = mapper;
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
                //Mapper
                var cliente = _mapper.Map<Cliente>(clienteDTO);

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
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCliente(int id, ClienteDTO clienteDTO)
        {
            DataResponse oResposne = new DataResponse();
            if(id!=clienteDTO.Id)
                return NotFound(oResposne);
            try
            {
                Cliente client = _mapper.Map<Cliente>(clienteDTO);
                _context.Entry(client).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                oResposne.Success=1;
                oResposne.Data = client;
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
                var clienteModel = await _context.Clientes.FindAsync(id);
                if(clienteModel==null){
                    return NotFound(oResponse);
                }
                Cliente cliente = _mapper.Map<Cliente>(clienteModel);
                _context.Remove(cliente);
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