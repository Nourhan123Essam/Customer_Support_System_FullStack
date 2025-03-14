using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSupport.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupport.Infrastructure.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // temporary we will complete on it later, but this for handle delete user 
        public async Task DeleteUserAsync(string userId)
        {
            ////////// when user is agent:

            //var tickets = await _dbContext.Tickets.Where(t => t.CustomerSupportUserId == userId).ToListAsync();
            //foreach (var ticket in tickets)
            //{
            //    ticket.CustomerSupportUserId = null;
            //}
            //await _dbContext.SaveChangesAsync();

            /////// when user is customer
            //var tickets = _dbContext.Tickets.Where(t => t.CustomerUserId == userId).ToList();
            //foreach (var ticket in tickets)
            //{
            //    ticket.CustomerUserId = null;
            //}
            //_dbContext.SaveChanges();

        }

    }
}
