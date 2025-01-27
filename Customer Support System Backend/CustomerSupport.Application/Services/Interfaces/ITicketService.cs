using CustomerSupport.Application.DTOs.Note;
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
        Task<List<NoteDTO>> GetTicketNotesAsync(int ticketId);
        public Task<int> CreateTicket(CreateTicketDTO ticketDTO, string userId);
    }
}
