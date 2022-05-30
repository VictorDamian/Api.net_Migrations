using ApiVentas.DAO;
using ApiVentas.Models;
using ApiVentas.Models.DTO;
using ApiVentas.Models.DTOs;
using ApiVentas.Models.Response;
using ApiVentas.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        public ClienteController(IClienteDAO clienteDAO, IMapper mapper)
        {
            _clienteDAO = clienteDAO;
            _mapper = mapper;
        }

        /// <sumary>
        /// Devulve una lista de clientes
        /// </sumary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAllClients()
        {
            DataResponse oResp = new DataResponse();
            var lstClientes = await _clienteDAO.GetAll();
            try
            {
                oResp.Success = 1;
                oResp.Data = lstClientes;
            }catch(Exception ex)
            {
                oResp.Messages = ex.Message;
            }

            return Ok(_mapper.Map<IEnumerable<ClienteReadDto>>(lstClientes));
        }
        [HttpGet("{id}", Name ="GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ClienteReadDto> GetId(int id)
        {
            DataResponse oResp = new DataResponse();
            
            var clienteId =  _clienteDAO.GetById(id);
            if(clienteId==null)
                return NotFound(oResp);
            oResp.Success = 1;
            oResp.Data = clienteId;
            return Ok(_mapper.Map<ClienteReadDto>(clienteId));
        }
        [HttpGet]
        [Route("nombre/{nombre}")]
        public ActionResult<IEnumerable<ClienteReadDto>> GetClientByName(String nombre)
        {
            DataResponse oData = new DataResponse();
            var clienteId = _clienteDAO.GetListByName(nombre);
            if(clienteId==null)
                return NotFound();
            oData.Success =1;
            oData.Data = clienteId;
            return Ok(_mapper.Map<IEnumerable<ClienteReadDto>>(clienteId));
        }
        [HttpPost]
        public async Task<ActionResult<ClienteReadDto>> PostClient(ClienteCreateDto clienteCreateDto)
        {
            DataResponse oData = new DataResponse();
            var model = _mapper.Map<Cliente>(clienteCreateDto);

            try
            {
                await _clienteDAO.Create(model);
                await _clienteDAO.Save();

                oData.Success = 1;
                oData.Data = model;
            }
            catch (Exception ex)
            {
                oData.Messages = ex.Message;
            }
            var clienteRead = _mapper.Map<ClienteReadDto>(model);
            return CreatedAtAction(nameof(GetId), new { Id = clienteRead.Id }, clienteRead);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCliente(int id, ClienteUpdateDto clienteUpdateDto)
        {
            DataResponse oResposne = new DataResponse();
            var clienteId = _clienteDAO.GetById(id);
            if(clienteId==null)
                return NotFound(oResposne);
            try
            {
                var model = _mapper.Map(clienteUpdateDto, clienteId);
                await _clienteDAO.Update(clienteId);
                oResposne.Success=1;
                oResposne.Data = model;
            }catch(Exception ex)
            {
                oResposne.Messages = ex.Message;
            }            
            return Ok(oResposne);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCliente(int id)
        {
            DataResponse oResponse = new DataResponse();
            var clienteId = _clienteDAO.GetById(id);
            try{
                if(clienteId == null)
                    return NotFound(oResponse);
                oResponse.Success = 1;
                oResponse.Data = clienteId;
            }catch(Exception ex){
                oResponse.Messages = ex.Message;               
            }
            await _clienteDAO.Delete(clienteId);
            return Ok(oResponse);
        }
    }
}
            
            
            
            