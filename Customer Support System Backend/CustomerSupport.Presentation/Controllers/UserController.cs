using CustomerSupport.Application.DTOs.Note;
using CustomerSupport.Application.DTOs.Rating;
using CustomerSupport.Application.Services.Implementations;
using CustomerSupport.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CustomerSupport.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(ITicketService ticketService, IHttpContextAccessor httpContextAccessor)
        {
            _ticketService = ticketService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpOptions]
        public IActionResult Preflight()
        {
            return NoContent(); // Respond with 204 No Content
        }



        [HttpPost("add-note")]
        public async Task<IActionResult> AddNote([FromBody] AddNoteDTO addNoteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _ticketService.AddNoteAsync(addNoteDTO, userId);
                return Ok(new { Message = "Note added successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while adding the note.", Details = ex.Message });
            }
        }

        [HttpPost("add-rating")]
        public async Task<IActionResult> AddRating([FromBody] AddRatingDTO ratingDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var result = await _ticketService.AddRatingAsync(ratingDTO, userId);
                return result ? Ok(new { Message = "Rating added successfully" }) : BadRequest(new { Message = "Failed to add rating" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("my-tickets")]
        public async Task<IActionResult> GetUserTickets()
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //if (string.IsNullOrEmpty(userId))
            //    return Unauthorized(new { Message = "Invalid user ID" });

            //var tickets = await _ticketService.GetUserTicketsAsync(userId);
            //return Ok(tickets);

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            if (userId == null || userRole == null)
                return Unauthorized();

            var tickets = await _ticketService.GetTicketsAsync(userRole, userId);
            return Ok(tickets);
        }

    }
}
