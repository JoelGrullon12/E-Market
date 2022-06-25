using E_Market.Core.Application.Helpers;
using E_Market.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;
namespace E_Market.Middleware
{
    public class ValidateSession
    {
        private readonly IHttpContextAccessor _httpContext;

        public ValidateSession(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public bool HasUser()
        {
            UserViewModel user = _httpContext.HttpContext.Session.Get<UserViewModel>("user");

            if (user == null)
                return false;

            return true;
        }
    }
}
