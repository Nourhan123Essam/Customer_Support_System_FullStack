using AutoMapper;
using CustomerSupport.Application.DTOs.Note;
using CustomerSupport.Application.DTOs.Ticket;
using CustomerSupport.Application.Services.Interfaces;
using CustomerSupport.Domain.Entities;
using CustomerSupport.Domain.Interfaces;

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

            //// Parsing Enums
            //if (!Enum.TryParse<TicketStatus>(ticketDTO.Status, true, out var status))
            //    throw new ArgumentException("Invalid status");

            //if (!Enum.TryParse<TicketPriority>(ticketDTO.Priority, true, out var priority))
            //    throw new ArgumentException("Invalid priority");

            //ticket.Status = status;
            //ticket.Priority = priority;

            await _ticketRepository.AddAsync(ticket);
            return ticket.Id;
        }

        public async Task<List<NoteDTO>> GetTicketNotesAsync(int ticketId)
        {
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

    }
}
