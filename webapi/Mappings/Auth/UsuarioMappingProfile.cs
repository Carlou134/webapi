using AutoMapper;
using webapi.DTOs;
using webapi.Models;

namespace webapi.Mappings.Auth
{
    public class UsuarioMappingProfile : Profile
    {
        public UsuarioMappingProfile()
        {
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(
                dest => dest.Rol,
                opt => opt.MapFrom(src => src.Rol!.NombreRol)
                );
        }
    }
}
