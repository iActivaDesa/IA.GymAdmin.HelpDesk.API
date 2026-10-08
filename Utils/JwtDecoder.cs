using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IA.GymAdmin.HelpDesk.API.Utils
{
    /// <summary>
    /// Decodificador de JWT para obtener el Id del usuario autenticado.
    /// El token se genera en <c>AuthService.GenerarToken</c> con el claim
    /// <c>ClaimTypes.NameIdentifier = empleado.Id</c>.
    /// Como el controller usa [Authorize], el JWT ya viene validado y
    /// el Id puede leerse desde <c>ControllerBase.User</c> sin re-validar la firma.
    /// </summary>
    public static class JwtDecoder
    {
        /// <summary>
        /// Obtiene el Id del usuario desde el ClaimsPrincipal ya validado por [Authorize].
        /// Busca en orden: NameIdentifier, "sub", "id", "nameid", ClaimTypes.Name.
        /// </summary>
        public static string? GetUserId(ClaimsPrincipal? user)
        {
            if (user?.Identity?.IsAuthenticated != true)
                return null;

            return user.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? user.FindFirstValue(JwtRegisteredClaimNames.Sub)
                ?? user.FindFirstValue("id")
                ?? user.FindFirstValue("nameid")
                ?? user.FindFirstValue(ClaimTypes.Name);
        }

        /// <summary>
        /// Decodifica un JWT crudo (con o sin prefijo "Bearer ") y devuelve su payload
        /// como <see cref="JwtSecurityToken"/> SIN validar firma ni expiración.
        /// Útil para inspección/debug. Para autorización usar <see cref="GetUserId"/>.
        /// </summary>
        public static JwtSecurityToken? Decode(string? token)
        {
            var raw = Normalize(token);
            if (string.IsNullOrWhiteSpace(raw))
                return null;

            try
            {
                return new JwtSecurityTokenHandler().ReadJwtToken(raw);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Decodifica un JWT crudo y extrae el Id del usuario directamente del payload,
        /// sin necesidad de HttpContext. No valida firma: solo lectura.
        /// </summary>
        public static string? GetUserIdFromToken(string? token)
        {
            var jwt = Decode(token);
            if (jwt == null)
                return null;

            return jwt.Claims.FirstOrDefault(c =>
                    c.Type == ClaimTypes.NameIdentifier ||
                    c.Type == JwtRegisteredClaimNames.Sub ||
                    c.Type == "id" ||
                    c.Type == "nameid")?.Value;
        }

        /// <summary>
        /// Extrae el token del header Authorization y devuelve el Id del usuario.
        /// Acepta HttpRequest para poder usarse fuera del controller si se necesita.
        /// </summary>
        public static string? GetUserIdFromRequest(HttpRequest? request, ClaimsPrincipal? fallbackUser = null)
        {
            // 1. Preferir el principal ya validado por [Authorize].
            var fromClaims = GetUserId(fallbackUser);
            if (!string.IsNullOrWhiteSpace(fromClaims))
                return fromClaims;

            // 2. Fallback: decodificar el header Authorization manualmente.
            var authHeader = request?.Headers.Authorization.ToString();
            return GetUserIdFromToken(authHeader);
        }

        /// <summary>
        /// Versión estricta: lanza <see cref="UnauthorizedAccessException"/> si no hay Id.
        /// Úsala en los controllers para fallar rápido con 401.
        /// </summary>
        public static string GetUserIdOrThrow(ClaimsPrincipal? user, HttpRequest? request = null)
        {
            var userId = GetUserId(user) ?? GetUserIdFromToken(request?.Headers.Authorization.ToString());

            if (string.IsNullOrWhiteSpace(userId))
                throw new UnauthorizedAccessException("No se pudo obtener el Id del usuario desde el token JWT.");

            return userId;
        }

        private static string? Normalize(string? token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            token = token.Trim();
            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                token = token["Bearer ".Length..].Trim();

            return token;
        }
    }
}
