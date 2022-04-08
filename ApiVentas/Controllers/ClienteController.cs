using ApiVentas.DAO;
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
        private readonly IClienteDAO _clienteDAO;
        private readonly IMapper _mapper;
        public ClienteController(IClienteDAO context, IMapper mapper)
        {
            _clienteDAO=context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            DataResponse data = new DataResponse();
            try
            {
                var clientes = await _clienteDAO.GetClienteAsync();
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
                //DAO
                await _clienteDAO.CreateCliente(cliente);
                oData.Success=1;
                oData.Data =cliente;
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
                await _clienteDAO.UpdateCliente(id, client);
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
                //Cliente cliente = _mapper.Map<Cliente>(id);
                
                await _clienteDAO.DeleteClinete(id);

                oResponse.Success = 1;
                //oResponse.Data = cliente;
            }catch(Exception ex){
                oResponse.Messages = ex.Message;               
            }
            return Ok(oResponse);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            DataResponse oResp = new DataResponse();
            try{
                var c = await _clienteDAO.GetClienteById(id);
                oResp.Success = 1;
                oResp.Data = c;
            }catch(Exception){throw;}
            return Ok(oResp);
        }
    }
}