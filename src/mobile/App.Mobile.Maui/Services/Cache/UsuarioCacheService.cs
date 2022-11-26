namespace App.Mobile.Maui.Services.Cache
{
    sealed class UsuarioCacheService
    {
        private static readonly Lazy<UsuarioCacheService> _cache = new(() => new UsuarioCacheService());

        public static UsuarioCacheService Current => _cache.Value;

        #region AcessToken

        public async Task<string> GetUserToken()
        {
            var token = await CacheServiceBase.Current.GetCacheAsync(MessageKeys.keyCacheTokenUser);
            return token ?? "";
        }

        public async Task SetUserToken(string value)
        {
            await CacheServiceBase.Current.SetCacheAsync(MessageKeys.keyCacheTokenUser, value);
        }

        #endregion

        #region RefreshToken

        public async Task<string> GetUserRefreshToken()
        {
            var token = await CacheServiceBase.Current.GetCacheAsync(MessageKeys.keyCacheRefreshTokenUser);
            return token ?? "";
        }

        public async Task SetUserRefreshToken(string value)
        {
            await CacheServiceBase.Current.SetCacheAsync(MessageKeys.keyCacheRefreshTokenUser, value);
        }

        #endregion

        #region Email

        public async Task SetEmail(string value)
        {
            await CacheServiceBase.Current.SetCacheAsync(MessageKeys.keyCacheEmailUser, value);
        }

        public async Task<string> GetEmail()
        {
            return await CacheServiceBase.Current.GetCacheAsync(MessageKeys.keyCacheEmailUser);
        }

        #endregion

        #region UserId

        public async Task SetUserId(string value)
        {
            await CacheServiceBase.Current.SetCacheAsync(MessageKeys.keyCacheUserId, value);
        }

        public async Task<string> GetUserId()
        {
            return await CacheServiceBase.Current.GetCacheAsync(MessageKeys.keyCacheUserId);
        }

        #endregion
    }
}
