﻿using System.Security.Claims;

namespace App.WebAPI.Core.Usuario
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal is null) throw new ArgumentException(null, nameof(principal));

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);

            return claim?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal is null) throw new ArgumentException(null, nameof(principal));

            var claim = principal.FindFirst("email");

            return claim?.Value;
        }

        public static string GetUserToken(this ClaimsPrincipal principal)
        {
            if (principal is null) throw new ArgumentException(null, nameof(principal));

            var claim = principal.FindFirst("JWT");

            return claim?.Value;
        }

        public static string GetUserRefreshToken(this ClaimsPrincipal principal)
        {
            if (principal is null) throw new ArgumentException(null, nameof(principal));

            var claim = principal.FindFirst("RefreshToken");

            return claim?.Value;
        }
    }
}