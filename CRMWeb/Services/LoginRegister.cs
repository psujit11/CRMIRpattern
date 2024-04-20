using CRMWeb.Interfaces;
using CRMWeb.Models;
using ir.infrastructure.DTOs.User;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CRMWeb.Services
{
    public class LoginRegister : ILoginRegister
    {
        private readonly ILogger<LoginRegister> _logger;
        private readonly HttpClient _httpClient;
        private readonly ApiUrlOptions _apiUrlOptions;
        private readonly IHttpContextAccessor _httpcontextAccessor;

        public LoginRegister(ILogger<LoginRegister> logger,
                             HttpClient httpClient,
                             IOptions<ApiUrlOptions> Options,
                             IHttpContextAccessor httpcontextAccessor)
        {
            _logger = logger;
            _httpClient = httpClient;
            _apiUrlOptions = Options.Value;
            _httpcontextAccessor = httpcontextAccessor;
        }

        public async Task<string> LoginAsync(LoginDto model)
        {
            try
            {
                var token = _httpcontextAccessor.HttpContext.Request.Headers["Authorization"];
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                                                                                                token);
                var json = JsonConvert.SerializeObject(model);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_apiUrlOptions.LoginUrl}", data);

                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    return stringResponse;
                }
                else
                {
                    _logger.LogError("Error in LoginAsync");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LoginAsync");
                throw new Exception("Error in LoginAsync");
            }
        }

        public async Task<string> RegisterAsync(UserDto registerUser)
        {
            try
            {
                var token = _httpcontextAccessor.HttpContext.Request.Headers["Authorization"];
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                                                                                                token);
                var json = JsonConvert.SerializeObject(registerUser);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_apiUrlOptions.RegisterUrl}",
                                                           data);

                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    return stringResponse;
                }
                else
                {
                    _logger.LogError("Error in RegisterAsync");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RegisterAsync");
                throw new Exception("Error in RegisterAsync");
            }
        }
    }
}
