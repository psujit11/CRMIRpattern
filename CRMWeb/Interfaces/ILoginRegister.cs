using ir.infrastructure.DTOs.User;

namespace CRMWeb.Interfaces
{
    public interface ILoginRegister
    {
        Task<string> RegisterAsync(UserDto registerUser);
        Task<string> LoginAsync(LoginDto model);
    }
}
