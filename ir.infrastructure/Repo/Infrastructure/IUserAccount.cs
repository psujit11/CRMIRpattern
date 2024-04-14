using ir.infrastructure.DTOs.User;
using static ir.infrastructure.DTOs.User.ServiceResponses;

namespace ir.infrastructure.Repo.Infrastructure
{

    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAccount(UserDto userDTO);
        Task<LoginResponse> LoginAccount(LoginDto loginDTO);

        Task<GeneralResponse> AddNewAdmin(UserDto adminDTO);
    }

}
