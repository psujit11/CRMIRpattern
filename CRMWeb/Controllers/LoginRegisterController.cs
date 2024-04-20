using CRMWeb.Interfaces;
using ir.infrastructure.DTOs.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static ir.infrastructure.DTOs.User.ServiceResponses;

namespace CRMWeb.Controllers
{
    public class LoginRegisterController : Controller
    {
        private readonly ILoginRegister _loginRegister;
        private readonly ITokenProvider _tokenProvider;

        public LoginRegisterController(ILoginRegister loginRegister, ITokenProvider tokenProvider)
        {
            _loginRegister = loginRegister;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDto registerUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   var result = await _loginRegister.RegisterAsync(registerUser);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(registerUser);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(registerUser);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Call Service to Login and Get JWT Token
                    var result = await _loginRegister.LoginAsync(model);

                    var loginResult = JsonConvert.DeserializeObject<LoginResponse>(result);
                    var token = loginResult.Token;

                    await SignInUser(token);
                    _tokenProvider.SetToken(token);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            _tokenProvider.ClearToken();
            return RedirectToAction("Login", "LoginRegister");
        }

        private async Task SignInUser(string token)
        {
            if(token == null)
            {
                throw new ArgumentNullException(nameof(token), "Token cannot be null");
            }
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            var nameClaim = jwt.Claims.FirstOrDefault(u => u.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
            if(nameClaim != null)
            {
                identity.AddClaim(new Claim(ClaimTypes.Name, nameClaim.Value));
            }
            else
            {
                identity.AddClaim(new Claim(ClaimTypes.Name, "Unknown"));
            }

            var roleClaim = jwt.Claims.FirstOrDefault(u => u.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            if (roleClaim != null)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, roleClaim.Value));
            }
            else
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
            }

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }        
    }

}
