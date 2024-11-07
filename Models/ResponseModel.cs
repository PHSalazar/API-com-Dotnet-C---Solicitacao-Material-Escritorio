namespace API_SolicitacaoMaterialEscritorio.Models
{
    public class ResponseModel<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; } = true;
    }
}
