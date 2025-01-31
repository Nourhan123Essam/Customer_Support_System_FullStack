
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthRepository(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        private async Task<IdentityUser> GetUserByEmailAsync(string email)
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
            //check if exist
            var getUser = await GetUserByEmailAsync(appUserDTO.Email);
            if (getUser is not null)
                return new Response(false, $"This email already registred");

            var newUser = new IdentityUser()
            {
                Email = appUserDTO.Email,
                UserName = appUserDTO.Name,
                PhoneNumber = appUserDTO.TelephoneNumber
            };
            var user = await _userManager.CreateAsync(newUser, appUserDTO.Password);
            return user.Succeeded ?
                new Response(true, "User registered successfully") :
                new Response(false, "Invalid data provided");
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
            string token = GenerateToken(getUser);
            return new Response(true, token);
        }

        private string GenerateToken(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                //new Claim(JwtRegisteredClaimNames.Sub, user.Id), // User's unique ID
                //new Claim(JwtRegisteredClaimNames.Name, user.UserName), // User's username
                //new Claim(JwtRegisteredClaimNames.Email, user.Email), // User's email
                //new Claim(ClaimTypes.Role, "User") // Optional: User's role

                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserType", "User")
            };

            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value);
            var sercurityKy = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(sercurityKy, SecurityAlgorithms.HmacSha256);
           
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(24),
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
