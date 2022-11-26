namespace App.Pedidos.API.Application.DTO
{
    public class VoucherDTO
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public decimal? Percentual { get; set; }
        public decimal? ValorDesconto { get; set; }
        public int TipoDesconto { get; set; }
        public int Quantidade { get; set; }
        public bool Ativo { get; set; }
        public bool Excluido { get; set; }
    }
}