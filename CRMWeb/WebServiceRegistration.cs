using CRMWeb.Interfaces;
using CRMWeb.Models;
using CRMWeb.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CRMWeb
{
    public static class WebServiceRegistration
    {
        public static IServiceCollection AddWebServiceRegistration(this IServiceCollection services,
                                                                   IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient();

            services.AddOptions<ApiUrlOptions>().BindConfiguration("ApiUrlOptions")
             .ValidateDataAnnotations()
             .ValidateOnStart();
            services.AddScoped<ILoginRegister, LoginRegister>();           
            services.AddScoped<ITokenProvider, TokenProvider>();          


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Adjust expiration time as needed
                    options.LoginPath = "/LoginRegister/Login"; // Change to your login path
                    options.AccessDeniedPath = "/LoginRegister/AccessDenied"; // Change to your access denied path
                    options.SlidingExpiration = true;
                });


            return services;
        }
    }
}
