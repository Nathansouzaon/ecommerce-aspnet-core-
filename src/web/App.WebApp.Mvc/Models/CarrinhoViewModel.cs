namespace App.WebApp.Mvc.Models
{
    public class CarrinhoViewModel
    {
        public decimal ValorTotal { get; set; }
        public VoucherViewModel Voucher { get; set; }
        public bool VoucherUtilizado { get; set; }
        public decimal Desconto { get; set; }
        public List<ItemCarrinhoViewModel> Itens { get; set; } = new List<ItemCarrinhoViewModel>();
    }
}
