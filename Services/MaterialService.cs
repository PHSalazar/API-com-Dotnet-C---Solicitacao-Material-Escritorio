using API_SolicitacaoMaterialEscritorio.DTO;
using API_SolicitacaoMaterialEscritorio.Models;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.DataProtection.Repositories;
using System.Data.SqlClient;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace API_SolicitacaoMaterialEscritorio.Services
{
    public class MaterialService : IMaterial
    {
        private IConfiguration _configuration;
        private readonly IMapper _mapper;

        public MaterialService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<ResponseModel<List<Material>>> GetAll()
        {
            ResponseModel<List<Material>> response = new ResponseModel<List<Material>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultString")))
            {

                var materiais = await connection.QueryAsync<Material>("SELECT * FROM Materiais");

                if (materiais.Count() == 0)
                {
                    response.Message = "Materiais não encontrados.";
                    response.IsSuccess = false;
                    return response;
                }

                var materiaisMapper = _mapper.Map<List<Material>>(materiais);
                response.Data = materiaisMapper;
                response.Message = materiais.Count() + " materiais encontrados.";
                return response;

            }
        }


        public async Task<ResponseModel<Material>> GetMaterialById(int id)
        {
            ResponseModel<Material> response = new ResponseModel<Material>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultString")))
            {
                var material = await connection.QueryFirstOrDefaultAsync<Material>("SELECT * FROM Materiais WHERE Id = @id", new { id = id });
                if (material == null)
                {
                    response.Message = "Material com o ID " + id + " não encontrado.";
                    response.IsSuccess = false;
                    return response;
                }

                
                response.Data = material;
                response.Message = "Material encontrado com sucesso.";
                return response;
            }
        }

        public async Task<ResponseModel<Material>> SolicitarMaterial(int id, int quantidade)
        {
            ResponseModel<Material> response = new ResponseModel<Material>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultString")))
            {

                var resultValidacao = await ValidacaoMaterial(connection, response, id, quantidade);

                if (resultValidacao.IsSuccess == false)
                {
                    response.Message = "Ocorreu um erro ao solicitar o material. Erro: " + response.Message;
                    return response;
                }

                bool decrementarQuantidadeBD = await DecrementarQuantidadeMaterialBD(connection, id, quantidade);

                if (decrementarQuantidadeBD == false)
                {
                    response.Message = "Não foi possível decrementar a quantidade do material solicitado.";
                    response.IsSuccess = false;
                    return response;
                }

                var material = await GetMaterialById(id);
                response.Data = material.Data;
                response.Message = "Solicitada(s) " + quantidade + " unidades do material (ID: " + id + ") com sucesso.";
                return response;
            }
        }

        

        private async static Task<ResponseModel<Material>> ValidacaoMaterial(SqlConnection connection, ResponseModel<Material> response, int id, int quantidade)
        {
            var materialExiste = await MaterialExiste(connection, id);
            if (materialExiste == false)
            {
                response.Message = "Não foi encontrado nenhum material com o ID " + id + ".";
                response.IsSuccess = false;
                return response;
            }

            var materialDisponivelNaQuantidadeSolicitada = await VerificarDisponibilidadeMaterial(connection, id, quantidade);
            if (materialDisponivelNaQuantidadeSolicitada == false)
            {
                response.Message = "Material não está disponível na quantidade solicitada.";
                response.IsSuccess = false;
                return response;
            }

            response.IsSuccess = true;
            return response;

        }

        private async static Task<bool> VerificarDisponibilidadeMaterial(SqlConnection connection, int id, int quantidadeSolicitada)
        {
            

            int quantidadeDisponivelMaterial = await connection.ExecuteScalarAsync<int>("SELECT QuantidadeDisponivel FROM Materiais WHERE Id = @id", new { id = id });
            
            if (quantidadeDisponivelMaterial < quantidadeSolicitada)
            {
                return false;
            }

            return true;

        }

        private async static Task<bool> MaterialExiste(SqlConnection connection, int id)
        {
            var sqlcmmd = await connection.QueryFirstOrDefaultAsync("SELECT * FROM Materiais WHERE Id = @id", new { id = id });

            if (sqlcmmd == null)
            {
                return false;
            }

            return true;
        }

        private async static Task<bool> DecrementarQuantidadeMaterialBD(SqlConnection connection, int id, int quantidade)
        {
            var sqlCommand = await connection.ExecuteAsync("UPDATE Materiais SET QuantidadeDisponivel = QuantidadeDisponivel - @quantidade WHERE ID = @id", new { id = id, quantidade = quantidade });

            if (sqlCommand == 0)
            {
                return false;
            }

            return true;
        }
    }
}
