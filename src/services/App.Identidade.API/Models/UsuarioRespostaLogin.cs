namespace App.Identidade.API.Models
{
    public class UsuarioRespostaLogin
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public Guid RefreshToken { get; set; }
        public UsuarioToken UsuarioToken { get; set; }
    }
}