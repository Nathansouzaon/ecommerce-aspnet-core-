using App.Mobile.Maui.Services.Navigation;

namespace App.Mobile.Maui
{
    public partial class App : Application
    {
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new();

            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost")) return true;

                return errors == System.Net.Security.SslPolicyErrors.None;
            };

            return handler;
        }

        public App()
        {
            InitializeComponent();
            NavigationService.Current.Initialize();

#if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            HttpClient client = new(insecureHandler);
#else
            HttpClient client = new HttpClient();
#endif
        }
    }
}