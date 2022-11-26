using App.Core.Communication;

namespace App.Mobile.Maui.Models
{
    public class UsuarioRegistro
    {
        public string Nome { get; set; } = null!;
        public string Cpf { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public string SenhaConfirmacao { get; set; } = null!;
    }

    public class UsuarioLogin
    {
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
    }

    public class UsuarioRespostaLogin
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public double ExpiresIn { get; set; }
        public UsuarioToken UsuarioToken { get; set; } = null!;
        public ResponseResult ResponseResult { get; set; } = null!;
    }

    public class UsuarioToken
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IEnumerable<UsuarioClaim> Claims { get; set; } = null!;
    }

    public class UsuarioClaim
    {
        public string? Value { get; set; }
        public string? Type { get; set; }
    }
}
