using API_SolicitacaoMaterialEscritorio.DTO;
using API_SolicitacaoMaterialEscritorio.Models;

namespace API_SolicitacaoMaterialEscritorio.Services
{
    public interface IMaterial
    {
        Task<ResponseModel<List<Material>>> GetAll();
        Task<ResponseModel<Material>> GetMaterialById(int id);
        Task<ResponseModel<Material>> SolicitarMaterial(int id, int quantidade);
    }
}
