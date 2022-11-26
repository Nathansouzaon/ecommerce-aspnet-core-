using System.Text.Json.Serialization;

namespace App.Backoffice.Mvc.Models
{
    public class ProdutoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Imagem { get; set; }
        public int QuantidadeEstoque { get; set; }
        public bool Excluido { get; set; }

        [JsonIgnore]
        public IFormFile ImagemForumlario { get; set; }
        public string ImagemUpload { get; set; }
    }
}