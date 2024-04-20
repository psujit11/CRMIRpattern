using CRMWeb.Interfaces;

namespace CRMWeb.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpcontextAccessor;
        public TokenProvider(IHttpContextAccessor httpcontextAccessor)
        {
            _httpcontextAccessor = httpcontextAccessor;
        }
        public void ClearToken()
        {
            _httpcontextAccessor.HttpContext?.Response.Cookies.Delete("token");
        }

        public string? GetToken()
        {
            string? token = null;
            bool? hasToken = _httpcontextAccessor.HttpContext?.Request.Cookies.TryGetValue("token", out token);
            return hasToken == true ? token : null;
        }

        public void SetToken(string token)
        {
           _httpcontextAccessor.HttpContext?.Response.Cookies.Append("token", token);
        }
    }
}
