using API_SolicitacaoMaterialEscritorio.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_SolicitacaoMaterialEscritorio.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterial _imaterial;
        public MaterialController(IMaterial material)
        {
            _imaterial = material;
        }


        /// <summary>
        /// Retorna todos os materiais do banco de dados.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var materiais = await _imaterial.GetAll();

            if (materiais.IsSuccess == false)
            {
                return NotFound(materiais);
            }

            return Ok(materiais);
        }

        /// <summary>
        /// Retorna um material com um ID específico.
        /// </summary>
        /// <param name="id">ID do material desejado.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaterialById(int id)
        {
            var material = await _imaterial.GetMaterialById(id);

            if (material.IsSuccess == false)
            {
                return NotFound(material);
            }

            return Ok(material);
        }

        /// <summary>
        /// Solicita um material, caso a solicitação seja efetuada com sucesso, retorna o material e uma mensagem de sucesso, caso não se possível solicitar o material, retorna a mensagem de erro.
        /// </summary>
        /// <param name="id">ID do material desejado.</param>
        /// <param name="quantidade">Quantidade desejada.</param>
        /// <returns></returns>
        [HttpGet("{id},{quantidade}")]
        public async Task<IActionResult> SolicitarMaterial(int id, int quantidade)
        {
            var material = await _imaterial.SolicitarMaterial(id, quantidade);

            if (material.IsSuccess == false)
            {
                return NotFound(material);
            }

            return Ok(material);
        }

    }
}
