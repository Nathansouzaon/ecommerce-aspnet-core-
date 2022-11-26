using App.Mobile.Maui.Models;
using App.Mobile.Maui.Services.Clients;

namespace App.Mobile.Maui.ViewModels
{
    public class ProdutoDetalhePageViewModel : BaseViewModel
    {
        private Produto _produto = null!;
        public Produto Produto
        {
            get => _produto;
            set => SetProperty(ref _produto, value);
        }

        public override async Task Initialize(object? args = null)
        {
            if (args is null)
            {
                //TODO: implementar tratamento de erros..
            }

            var produtoId = (Guid)args!;
            var produto = await CatalogoService.Current.ObterPorId(produtoId);
            Produto = produto;

            await base.Initialize(args);
        }
    }
}