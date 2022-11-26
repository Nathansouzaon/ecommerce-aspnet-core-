using System.ComponentModel.DataAnnotations;

namespace App.Backoffice.Mvc.Models
{
    public class VoucherViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Codigo { get; set; } = null!;

        public decimal? Percentual { get; set; }

        public decimal? ValorDesconto { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int TipoDesconto { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataCriacao { get; set; }

        public DateTime? DataUtilizacao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataValidade { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool Ativo { get; set; }

        public bool Excluido { get; set; }
    }
}