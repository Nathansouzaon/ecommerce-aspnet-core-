using App.Mobile.Maui.Models;
using App.Mobile.Maui.Services.Clients;
using App.Mobile.Maui.Services.Navigation;
using CommunityToolkit.Mvvm.Input;

namespace App.Mobile.Maui.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        #region Propriedades

        private IEnumerable<Produto> _produtos = new List<Produto>();
        public IEnumerable<Produto> Produtos
        {
            get => _produtos;
            set => SetProperty(ref _produtos, value);
        }

        #endregion

        #region Commands

        private AsyncRelayCommand<Produto>? _produtoDetalheCommand;
        public AsyncRelayCommand<Produto> ProdutoDetalheCommand
            => _produtoDetalheCommand
            ??= new AsyncRelayCommand<Produto>(
                execute: ProdutoDetalheCommandExecute,
                canExecute: ProdutoDetalheCommandCanExecute);

        public bool ProdutoDetalheCommandCanExecute(Produto? produto)
        {
            return true;
        }

        async Task ProdutoDetalheCommandExecute(Produto? produto)
        {
            await NavigationService.Current.Navegar<ProdutoDetalhePageViewModel>(produto?.Id);
        }

        private AsyncRelayCommand<Produto>? _irMeuCarrinhoCommand;
        public AsyncRelayCommand<Produto> IrMeuCarrinhoCommand
            => _irMeuCarrinhoCommand
            ??= new AsyncRelayCommand<Produto>(
                execute: IrCarrinhoCommandExecute);

        async Task IrCarrinhoCommandExecute(Produto? produto)
        {
            await NavigationService.Current.Navegar<CarrinhoPageViewModel>();
        }

        #endregion

        public override async Task Initialize(object? args = null)
        {
            var produtos = await CatalogoService.Current.ObterTodos(9, 1);
            Produtos = produtos.List;
        }
    }
}