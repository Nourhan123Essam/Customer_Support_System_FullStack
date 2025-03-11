using AutoMapper;
using CustomerSupport.Application.DTOs.Ticket;
using CustomerSupport.Domain.Entities;

namespace CustomerSupport.Application.Mapping
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<Ticket, TicketDTO>().ReverseMap();
            CreateMap<CreateTicketDTO, Ticket>().ReverseMap();
            CreateMap<Ticket, GetTicketDTO>().ReverseMap();
        }
    }
}
