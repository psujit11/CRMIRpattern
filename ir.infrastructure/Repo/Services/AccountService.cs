using ir.domain.Entities;
using ir.infrastructure.DTOs.User;
using ir.infrastructure.Repo.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static ir.infrastructure.DTOs.User.ServiceResponses;

namespace ir.infrastructure.Repo.Services
{
    public class AccountService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config) : IUserAccount
    {
        public async Task<GeneralResponse> CreateAccount(UserDto userDTO)
        {
            if (userDTO is null) return new GeneralResponse(false, "Model is empty");
            var newUser = new ApplicationUser()
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                PasswordHash = userDTO.Password,
                UserName = userDTO.Email
            };
            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user is not null) return new GeneralResponse(false, "User registered already");

            var createUser = await userManager.CreateAsync(newUser!, userDTO.Password);
            if (!createUser.Succeeded) return new GeneralResponse(false, "Error occured.. please try again");

            //Assign Default Role : Admin to first registrar; rest is user
            var checkAdmin = await roleManager.FindByNameAsync("Admin");
            if (checkAdmin is null)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await userManager.AddToRoleAsync(newUser, "Admin");
                return new GeneralResponse(true, "Account Created");
            }
            else
            {
                var checkUser = await roleManager.FindByNameAsync("User");
                if (checkUser is null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = "User" });

                await userManager.AddToRoleAsync(newUser, "User");
                return new GeneralResponse(true, "Account Created");
            }
        }

        public async Task<GeneralResponse> AddNewAdmin(UserDto adminDTO)
        {
            // Check if the adminDTO is null
            if (adminDTO is null)
                return new GeneralResponse(false, "Model is empty");

            // Check if the user with the provided email already exists
            var existingUser = await userManager.FindByEmailAsync(adminDTO.Email);
            if (existingUser != null)
                return new GeneralResponse(false, "Admin with this email already exists");

            // Create a new ApplicationUser instance
            var newAdmin = new ApplicationUser()
            {
                Name = adminDTO.Name,
                Email = adminDTO.Email,
                PasswordHash = adminDTO.Password,
                UserName = adminDTO.Email
            };

            // Create the new admin user in the user manager
            var createAdminResult = await userManager.CreateAsync(newAdmin, adminDTO.Password);
            if (!createAdminResult.Succeeded)
                return new GeneralResponse(false, "Error occurred while creating admin account");

            // Assign the "Admin" role to the new user
            var adminRole = await roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                // If the "Admin" role doesn't exist, create it
                adminRole = new IdentityRole("Admin");
                await roleManager.CreateAsync(adminRole);
            }

            // Add the "Admin" role to the new admin user
            var addToRoleResult = await userManager.AddToRoleAsync(newAdmin, "Admin");
            if (!addToRoleResult.Succeeded)
                return new GeneralResponse(false, "Failed to assign admin role to the new user");

            // Return a successful response
            return new GeneralResponse(true, "New admin account created successfully");
        }

        public async Task<LoginResponse> LoginAccount(LoginDto loginDTO)
        {
            if (loginDTO == null)
                return new LoginResponse(false, null!, "Login container is empty");

            var getUser = await userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser is null)
                return new LoginResponse(false, null!, "User not found");

            bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkUserPasswords)
                return new LoginResponse(false, null!, "Invalid email/password");

            var getUserRole = await userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.Name, getUser.Email, getUserRole.First());
            string token = GenerateToken(userSession);
            return new LoginResponse(true, token!, "Login completed");
        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
    
