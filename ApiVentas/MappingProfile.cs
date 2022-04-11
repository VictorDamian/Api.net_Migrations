using ApiVentas.Models;
using ApiVentas.Models.DTOs;
using AutoMapper;

namespace ApiVentas
{
    public class MappingProfile:Profile
    {
        public MappingProfile(){
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<ClienteDTO, Cliente>().ReverseMap();
        }
    }
}