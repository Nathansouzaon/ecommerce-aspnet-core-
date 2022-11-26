using App.Core.DomainObjects;

namespace App.Pagamentos.API.Models
{
    public class Transacao : Entity
    {
        public string CodigoAutorizacao { get; set; }
        public string BandeiraCartao { get; set; }
        public DateTime? DataTransacao { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal CustoTransacao { get; set; }
        public StatusTransacao Status { get; set; }
        public string TID { get; set; }
        public string NSU { get; set; } // Meio de pagamento

        public Guid PagamentoId { get; set; }

        // EFCore Relation
        public Pagamento Pagamento { get; set; }
    }
}
