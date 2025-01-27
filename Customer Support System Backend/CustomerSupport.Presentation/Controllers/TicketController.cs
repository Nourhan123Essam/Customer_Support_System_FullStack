using CustomerSupport.Application.DTOs.Ticket;
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
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("create")]      
        public async Task<IActionResult> CreateTicket([FromForm] CreateTicketDTO ticketDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ticketId = await _ticketService.CreateTicket(ticketDTO, userId);
            return Ok(new { TicketId = ticketId, Message = "Ticket created successfully" });
        }

        [HttpGet("{ticketId}/notes")]
        public async Task<IActionResult> GetTicketNotes(int ticketId)
        {
            try
            {
                var notes = await _ticketService.GetTicketNotesAsync(ticketId);
                return Ok(notes);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving notes.", Details = ex.Message });
            }
        }
    }
}
