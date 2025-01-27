﻿using CustomerSupport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSupport.Domain.Interfaces
{
    public interface ITicketRepository: IGenericRepository<Ticket>
    {
        Task AddAsync(Ticket ticket);
        Task<Ticket> GetByIdAsync(int id);
        Task<List<Note>> GetTicketNotesAsync(int ticketId);
        Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(int userId);
    }
}
