namespace App.Mobile.Maui.Services.Cache
{
    sealed class CacheServiceBase
    {
        private static readonly Lazy<CacheServiceBase> _Lazy = new(() => new CacheServiceBase());

        public static CacheServiceBase Current => _Lazy.Value;

        public CacheServiceBase() { }

        public async Task<string> GetCacheAsync(string key)
        {
            return await SecureStorage.GetAsync(key) ?? string.Empty;
        }

        public async Task SetCacheAsync(string key, string value)
        {
            await SecureStorage.SetAsync(key, value);
        }

        public void ClearCache()
        {
            SecureStorage.RemoveAll();
        }
    }
}
