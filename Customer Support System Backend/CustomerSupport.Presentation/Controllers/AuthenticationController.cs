using CustomerSupport.Application.DTOs.User;
using CustomerSupport.Application.Services.Interfaces;
using CustomerSupport.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSupport.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpOptions]
        public IActionResult Preflight()
        {
            return NoContent(); // Respond with 204 No Content
        }


        [HttpPost("register")]
        public async Task<ActionResult<Response>> Register(AppUserDTO appUserDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _authService.Register(appUserDTO);
            return result.Flag ? Ok(result) : BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Response>> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _authService.Login(loginDTO);
            return result.Flag ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<GetUserDTO>> GetUser(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("Invalid user Id");

            var user = await _authService.GetUser(id);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            return Ok(user);
        }
    }
}
