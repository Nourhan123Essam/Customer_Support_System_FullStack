using CustomerSupport.Application.DTOs.Note;
using CustomerSupport.Domain.Entities;
using CustomerSupport.Domain.Enums;
using CustomerSupport.Domain.Interfaces;
using CustomerSupport.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupport.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private static readonly Dictionary<string, Func<IQueryable<Ticket>, string, IQueryable<Ticket>>> TicketFilters =
        new()
        {
            { "Customer", (tickets, userId) => tickets.Where(t => t.CustomerUserId == userId) },
            { "SupportAgent", (tickets, userId) => tickets.Where(t => t.CustomerSupportUserId == userId) },
            { "Admin", (tickets, _) => tickets } // Admin sees all tickets
        };

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
                //.Include(t => t.Notes)
                .Include(t => t.Rating)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Note>> GetTicketNotesAsync(int ticketId)
        {
            var notes = await _dbContext.Notes
                .Where(note => note.TicketId == ticketId)
                .ToListAsync();

            return notes;
        }

        public async Task AddNoteAsync(Note note)
        {
            await _dbContext.Notes.AddAsync(note);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> IsTicketClosedAsync(int ticketId)
        {
            var ticket = await _dbContext.Tickets
                .Where(t => t.Id == ticketId)
                .Select(t => t.Status)
                .FirstOrDefaultAsync();

            return ticket == TicketStatus.Closed;
        }

        public async Task<Ticket> GetTicketWithRatingAsync(int ticketId)
        {
            return await _dbContext.Tickets
                .Include(t => t.Rating)
                .FirstOrDefaultAsync(t => t.Id == ticketId);
        }

        public async Task Update(Ticket ticket)
        {
            _dbContext.Tickets.Update(ticket);
            await _dbContext.SaveChangesAsync();
        }


        // from here we refactore the code
        public async Task<List<Ticket>> GetFilteredTicketsAsync(string userRole, string userId)
        {

            var tickets = _dbContext.Tickets.AsQueryable();

            if (TicketFilters.TryGetValue(userRole, out var filter))
            {
                tickets = filter(tickets, userId); // Apply role-based filter
            }

            return await tickets.ToListAsync(); // Execute query asynchronously


            //return await _dbContext.Tickets
            //    .Where(t => t.CustomerUserId == userId)
            //    .ToListAsync();
        }
    }
}
