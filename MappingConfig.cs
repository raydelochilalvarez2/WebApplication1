using AutoMapper;
using WebApplication1.Modelos;
using WebApplication1.Modelos.DTO;

namespace WebApplication1
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>();
            CreateMap<VillaDTO, Villa>();

            CreateMap<Villa, VillaCreateDTO>().ReverseMap();         

            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
          
        }
    }
}
