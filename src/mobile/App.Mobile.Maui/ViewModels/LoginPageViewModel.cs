using App.Mobile.Maui.Models;
using App.Mobile.Maui.Services.Cache;
using App.Mobile.Maui.Services.Clients;
using App.Mobile.Maui.Services.Navigation;
using CommunityToolkit.Mvvm.Input;

namespace App.Mobile.Maui.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        #region Propriedades

        private string _email = null!;
        public string Email
        {
            get => _email;
            set => SetProperty(backingStore: ref _email, value: value, onChanged: () =>
            {
                LoginCommand.NotifyCanExecuteChanged();
            });
        }

        private string _senha = null!;
        public string Senha
        {
            get => _senha;
            set => SetProperty(backingStore: ref _senha, value: value, onChanged: () =>
            {
                LoginCommand.NotifyCanExecuteChanged();
            });
        }

        #endregion

        #region Commands

        private AsyncRelayCommand? _loginCommand;
        public AsyncRelayCommand LoginCommand
            => _loginCommand
            ??= new AsyncRelayCommand(
                execute: ValidarCommandExecute,
                canExecute: ValidarCommandCanExecute);

        public bool ValidarCommandCanExecute() =>
            string.IsNullOrEmpty(Email) is false &&
            string.IsNullOrEmpty(Senha) is false;

        async Task ValidarCommandExecute()
        {
            var usuario = new UsuarioLogin()
            {
                Email = Email,
                Senha = Senha
            };

            var result = await AutenticacaoService.Current.Login(usuario);

            if (result.ResponseResult?.Errors is not null)
            {
                //TODO: tratar caso de erro..
            }

            if (result is not null)
            {
                await UsuarioCacheService.Current.SetUserToken(result.AccessToken);
                await UsuarioCacheService.Current.SetUserRefreshToken(result.RefreshToken);
                await UsuarioCacheService.Current.SetEmail(result.UsuarioToken.Email);

                await NavigationService.Current.Navegar<HomePageViewModel>();
            }
        }

        private AsyncRelayCommand? _irRegistroCommand;
        public AsyncRelayCommand IrRegistroCommand
            => _irRegistroCommand
            ??= new AsyncRelayCommand(
                execute: IrRegistroCommandExecute,
                canExecute: IrRegistroCommandCanExecute);

        public bool IrRegistroCommandCanExecute()
        {
            return true;
        }
        async Task IrRegistroCommandExecute()
        {
            await NavigationService.Current.Navegar<CadastroPageViewModel>();
        }

        #endregion

        public override async Task Initialize(object? args = null)
        {
            var accessToken = await UsuarioCacheService.Current.GetUserToken();

            if (string.IsNullOrEmpty(accessToken)) return;

            await NavigationService.Current.Navegar<HomePageViewModel>();
        }
    }
}
