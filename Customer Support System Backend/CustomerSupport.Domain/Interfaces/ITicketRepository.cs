using CustomerSupport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSupport.Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task AddAsync(Ticket ticket);
        Task<Ticket> GetByIdAsync(int id);
        Task<List<Note>> GetTicketNotesAsync(int ticketId);
        Task AddNoteAsync(Note note);
        Task<Ticket> GetTicketWithRatingAsync(int ticketId);
        Task Update(Ticket ticket);
        Task<bool> IsTicketClosedAsync(int ticketId);
        Task<List<Ticket>> GetFilteredTicketsAsync(string userRole, string userId);
        Task SaveChanges();
    }
}
