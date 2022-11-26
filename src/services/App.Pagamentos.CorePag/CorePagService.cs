namespace App.Pagamentos.CorePag
{
    public class CorePagService
    {
        public readonly string ApiKey;
        public readonly string EncryptionKey;

        public CorePagService(string apiKey, string encryptionKey)
        {
            ApiKey = apiKey;
            EncryptionKey = encryptionKey;
        }
    }
}
