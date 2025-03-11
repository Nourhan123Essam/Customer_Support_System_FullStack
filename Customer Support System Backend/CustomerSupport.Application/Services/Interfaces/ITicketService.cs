using CustomerSupport.Application.DTOs.Note;
using CustomerSupport.Application.DTOs.Rating;
using CustomerSupport.Application.DTOs.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSupport.Application.Services.Interfaces
{
    public interface ITicketService
    {
        Task AddNoteAsync(AddNoteDTO addNoteDTO, string userId);
        Task<bool> AddRatingAsync(AddRatingDTO ratingDTO, string userId);
        public Task<List<GetTicketDTO>> GetTicketsAsync(string userRole, string userId);
        Task<List<NoteDTO>> GetTicketNotesAsync(int ticketId, string userId);
        Task<int> CreateTicket(CreateTicketDTO ticketDTO, string userId);
    }
}
