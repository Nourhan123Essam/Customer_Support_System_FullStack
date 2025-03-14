
using Microsoft.AspNetCore.Identity;
using CustomerSupport.Infrastructure.Data;
using System.Text;
using CustomerSupport.Application.Services.Interfaces;
using CustomerSupport.Domain.Responses;
using CustomerSupport.Application.DTOs.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace CustomerSupport.Infrastructure.Repositories
{
    public class AuthRepository: IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthRepository(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        private async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> AssignRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }

        public async Task<Response> Register(AppUserDTO appUserDTO)
        {
            // Check if the user already exists
            var getUser = await GetUserByEmailAsync(appUserDTO.Email);
            if (getUser is not null)
                return new Response(false, $"This email is already registered");

            // Create a new user
            var newUser = new ApplicationUser()
            {
                Email = appUserDTO.Email,
                UserName = appUserDTO.Name,
                PhoneNumber = appUserDTO.TelephoneNumber
            };

            var userResult = await _userManager.CreateAsync(newUser, appUserDTO.Password);
            if (!userResult.Succeeded)
                return new Response(false, "Invalid data provided");

            // Assign "Customer" role by default
            await _userManager.AddToRoleAsync(newUser, "Customer");

            return new Response(true, "User registered successfully");
        }


        public async Task<Response> Login(LoginDTO loginDTO)
        {
            //check if exist
            var getUser = await GetUserByEmailAsync(loginDTO.Email);
            if (getUser is null)
                return new Response(false, "Invalid credentials");

            //check of email correct
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
                return new Response(false, "Invalid Email");

            //check if password correct
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if (result == null)
            {
                return new Response(false, "Invalid Password");
            }
              
            //generate token and return it as the user vervified
            string token = await GenerateToken(getUser);
            return new Response(true, token);
        }

        private async Task<string> GenerateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            // Fetch the user's role from the database
            var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            if (!string.IsNullOrEmpty(userRole))
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value);
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<GetUserDTO> GetUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            return new GetUserDTO(
                user.Id,
                user.UserName,
                user.PhoneNumber,
                user.Email,
                role
            );
        }
    }
}
