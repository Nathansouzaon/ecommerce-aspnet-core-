namespace App.Catalogo.API.Dtos
{
    public class ProdutoAdicionarDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Imagem { get; set; }
        public string ImagemUpload { get; set; }
        public int QuantidadeEstoque { get; set; }
    }

    public class ProdutoAtualizarDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Imagem { get; set; }
        public string ImagemUpload { get; set; }
        public int QuantidadeEstoque { get; set; }
    }
}