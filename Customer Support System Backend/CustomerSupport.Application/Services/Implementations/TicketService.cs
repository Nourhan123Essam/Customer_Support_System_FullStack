using AutoMapper;
using CustomerSupport.Application.DTOs.Note;
using CustomerSupport.Application.DTOs.Rating;
using CustomerSupport.Application.DTOs.Ticket;
using CustomerSupport.Application.Services.Interfaces;
using CustomerSupport.Domain.Entities;
using CustomerSupport.Domain.Enums;
using CustomerSupport.Domain.Interfaces;
using System.Net.Sockets;
using System.Security.Claims;

namespace CustomerSupport.Application.Services.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public TicketService(IMapper mapper, ITicketRepository ticketRepository, IFileService fileService)
        {
            _ticketRepository = ticketRepository;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<int> CreateTicket(CreateTicketDTO ticketDTO, string userId)
        {
            var ticket = //_mapper.Map<Ticket>(ticketDTO);
           new Ticket
            {
                Title = ticketDTO.Title,
                Description = ticketDTO.Description,
                Priority = ticketDTO.Priority,
                Status = ticketDTO.Status,
                CustomerUserId = userId,
                CustomerSupportUserId = "b2c18365-e719-4e51-97e5-89d620e73959", //default customer support user
                CategoryId = ticketDTO.CategoryId,
                Attachments = new List<Attachment>()
            };

            // Handle file attachments
            if (ticketDTO.Attachments != null && ticketDTO.Attachments.Any())
            {
                foreach (var file in ticketDTO.Attachments)
                {
                    var fileName = await _fileService.SaveFileAsync(file, "tickets");
                    ticket.Attachments.Add(new Attachment { FilePath = fileName, FileName = file.FileName });
                }
            }

            await _ticketRepository.AddAsync(ticket);
            return ticket.Id;
        }

        public async Task<List<NoteDTO>> GetTicketNotesAsync(int ticketId, string userId)
        {
            // Get Ticket
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);

            // Check if the user Authorized
            if (ticket == null || ticket.CustomerUserId != userId)
                throw new Exception("Ticket not found or you do not have access to it.");

            var notes = await _ticketRepository.GetTicketNotesAsync(ticketId);
            if (notes == null)
                throw new KeyNotFoundException("Ticket not found.");

            var result = new List<NoteDTO>();
            foreach (var note in notes)
            {
                var newNote = new NoteDTO()
                {
                    Content = note.Content,
                    Id = note.NoteId,
                    CreatedAt = note.CreatedAt,
                    CreatedBy = note.UserId
                };
                result.Add(newNote);
            }

            return result;
        }

        public async Task AddNoteAsync(AddNoteDTO addNoteDTO, string userId)
        {
            // Get Ticket
            var ticket = await _ticketRepository.GetByIdAsync(addNoteDTO.TicketId);    
            
            // Check if the user Authorized
            if (ticket == null || ticket.CustomerUserId != userId)
                throw new Exception("Ticket not found or you do not have access to it.");

            // Check if ticket exists and is not closed
            var isClosed = await _ticketRepository.IsTicketClosedAsync(addNoteDTO.TicketId);
            if (isClosed)
                throw new InvalidOperationException("Cannot add a note to a closed ticket.");

            // Create and save the note
            var note = new Note
            {
                Content = addNoteDTO.Content,
                TicketId = addNoteDTO.TicketId,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };

            await _ticketRepository.AddNoteAsync(note);
        }

        public async Task<bool> AddRatingAsync(AddRatingDTO ratingDTO, string userId)
        {
            // Get the ticket
            var ticket = await _ticketRepository.GetTicketWithRatingAsync(ratingDTO.TicketId);

            if (ticket == null || ticket.CustomerUserId != userId)
                throw new Exception("Ticket not found or you do not have access to it.");

            if (ticket.Status != TicketStatus.Closed)
                throw new Exception("Cannot add a rating to a ticket that is not closed.");

            if (ticket.Rating != null)
                throw new Exception("This ticket already has a rating.");

            // Create the rating
            var rating = new Rating
            {
                Score = ratingDTO.Score,
                Feedback = ratingDTO.Feedback,
                TicketId = ratingDTO.TicketId,
                UserId = userId
            };

            ticket.Rating = rating; // Assign the rating to the ticket

            // Save the changes
            await _ticketRepository.Update(ticket);

            return true;
        }

        public async Task<IEnumerable<GetTicketDTO>> GetUserTicketsAsync(string userId)
        {
            var tickets = await _ticketRepository.GetTicketsByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<GetTicketDTO>>(tickets);
        }
    }
}
