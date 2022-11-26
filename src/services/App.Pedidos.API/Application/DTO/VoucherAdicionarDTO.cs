namespace App.Pedidos.API.Application.DTO
{
    public class VoucherAdicionarDTO
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public decimal? Percentual { get; set; }
        public decimal? ValorDesconto { get; set; }
        public int Quantidade { get; set; }
        public int TipoDesconto { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataUtilizacao { get; set; }
        public DateTime DataValidade { get; set; }
        public bool Ativo { get; set; }
        public bool Utilizado { get; set; }
    }
}
