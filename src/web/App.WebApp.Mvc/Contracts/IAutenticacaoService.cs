using App.WebApp.Mvc.Models;

namespace App.WebApp.Mvc.Contracts
{
    public interface IAutenticacaoService
    {
        Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin);
        Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro);

        Task RealizarLogin(UsuarioRespostaLogin resposta);
        Task Logout();
        bool TokenExpirado();
        Task<bool> RefreshTokenValido();
    }
}