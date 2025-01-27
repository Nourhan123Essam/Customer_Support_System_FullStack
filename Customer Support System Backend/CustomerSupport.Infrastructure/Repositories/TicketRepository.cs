using CustomerSupport.Application.DTOs.Note;
using CustomerSupport.Domain.Entities;
using CustomerSupport.Domain.Interfaces;
using CustomerSupport.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupport.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _dbContext;

        public TicketRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Ticket ticket)
        {
            await _dbContext.Tickets.AddAsync(ticket);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Ticket> GetByIdAsync(int id)
        {
            return await _dbContext.Tickets
                .Include(t => t.Attachments)
                .Include(t => t.Notes)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Note>> GetTicketNotesAsync(int ticketId)
        {
            var notes = await _dbContext.Notes
                .Where(note => note.TicketId == ticketId)
                .ToListAsync();

            return notes;
        }


        public Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
