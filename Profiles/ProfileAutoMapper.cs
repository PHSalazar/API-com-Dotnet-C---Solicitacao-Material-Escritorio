using API_SolicitacaoMaterialEscritorio.DTO;
using API_SolicitacaoMaterialEscritorio.Models;
using AutoMapper;

namespace API_SolicitacaoMaterialEscritorio.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<Material, Material>();
        }
    }
}
