using App.Mobile.Maui.Models;
using App.Mobile.Maui.Services.Cache;
using App.Mobile.Maui.Services.Clients;
using App.Mobile.Maui.Services.Navigation;
using CommunityToolkit.Mvvm.Input;

namespace App.Mobile.Maui.ViewModels
{
    internal class CadastroPageViewModel : BaseViewModel
    {
        #region Propriedades

        private string _nome = null!;
        public string Nome
        {
            get => _nome;
            set => SetProperty(ref _nome, value, onChanged: () => RegistroCommand.NotifyCanExecuteChanged());
        }

        private string _cpf = null!;
        public string Cpf
        {
            get => _cpf;
            set => SetProperty(ref _cpf, value, onChanged: () => RegistroCommand.NotifyCanExecuteChanged());
        }

        private string _email = null!;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value, onChanged: () => RegistroCommand.NotifyCanExecuteChanged());
        }

        private string _senha = null!;
        public string Senha
        {
            get => _senha;
            set => SetProperty(ref _senha, value, onChanged: () => RegistroCommand.NotifyCanExecuteChanged());
        }

        private string _senhaConfirmacao = null!;
        public string SenhaConfirmacao
        {
            get => _senhaConfirmacao;
            set => SetProperty(ref _senhaConfirmacao, value, onChanged: () => RegistroCommand.NotifyCanExecuteChanged());
        }

        #endregion

        #region Commands

        private AsyncRelayCommand? _registroCommand;
        public AsyncRelayCommand RegistroCommand
            => _registroCommand
            ??= new AsyncRelayCommand(
                execute: ValidarCommandExecute,
                canExecute: ValidarCommandCanExecute);

        public bool ValidarCommandCanExecute() =>
            string.IsNullOrEmpty(Nome) is false &&
            string.IsNullOrEmpty(Cpf) is false &&
            string.IsNullOrEmpty(Email) is false &&
            string.IsNullOrEmpty(Senha) is false &&
            string.IsNullOrEmpty(SenhaConfirmacao) is false;

        async Task ValidarCommandExecute()
        {
            var usuario = new UsuarioRegistro()
            {
                Nome = Nome,
                Cpf = Cpf,
                Email = Email,
                Senha = Senha,
                SenhaConfirmacao = SenhaConfirmacao
            };

            var result = await AutenticacaoService.Current.Registro(usuario);


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

        private AsyncRelayCommand? _irLoginCommand;
        public AsyncRelayCommand IrLoginCommand
            => _irLoginCommand
            ??= new AsyncRelayCommand(
                execute: IrLoginCommandExecute,
                canExecute: IrLoginCommandCanExecute);

        public bool IrLoginCommandCanExecute()
        {
            return true;
        }
        async Task IrLoginCommandExecute()
        {
            await NavigationService.Current.GoBack();
        }

        #endregion

        public override async Task Initialize(object? args = null)
        {
            await base.Initialize(args);
        }
    }
}
