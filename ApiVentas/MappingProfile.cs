using ApiVentas.Models;
using ApiVentas.Models.DTO;
using ApiVentas.Models.DTOs;
using AutoMapper;

namespace ApiVentas
{
    public class MappingProfile:Profile
    {
        public MappingProfile(){
            // Cliente
            CreateMap<Cliente, ClienteReadDto>();
            CreateMap<ClienteCreateDto, Cliente>();
            CreateMap<ClienteUpdateDto, Cliente>();
            CreateMap<Cliente, ClienteUpdateDto>();
        }
    }
}