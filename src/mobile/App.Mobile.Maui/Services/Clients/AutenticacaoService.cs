using App.Core.Communication;
using App.Mobile.Maui.Models;

namespace App.Mobile.Maui.Services.Clients
{
    sealed class AutenticacaoService : Service
    {
        #region Inicializacao lazy load
        private static readonly Lazy<AutenticacaoService> _autenticacaoService = new(() => new AutenticacaoService());
        public static AutenticacaoService Current => _autenticacaoService.Value;
        #endregion

        public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro)
        {
            var registroContent = ObterConteudo(usuarioRegistro);

            var response = await _httpClient
                .PostAsync($"{BaseAddress}:6101/api/api/identidade/nova-conta", registroContent);

            if (TratarErrosResponse(response) is false)
            {
                return new UsuarioRespostaLogin
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
        }

        public async Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin)
        {
            var loginContent = ObterConteudo(usuarioLogin);

            var response = await _httpClient
                .PostAsync($"{BaseAddress}:6101/api/identidade/autenticar", loginContent);

            if (TratarErrosResponse(response) is false)
            {
                return new UsuarioRespostaLogin
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
        }
    }
}
