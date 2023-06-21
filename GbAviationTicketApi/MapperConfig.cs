using AutoMapper;
using GbAviationTicketApi.Models;
using GbAviationTicketApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace GbAviationTicketApi
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Terminal, TerminalDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Paymentmthd, PaymentmthdDto>().ReverseMap();

            CreateMap<TicketDto, TicketCreateDto>().ReverseMap();
            CreateMap<TicketDto, TicketUpdateDto>().ReverseMap();

            CreateMap<Ticket, TicketDto>()
                .ForMember(dt => dt.InitTime, opt => opt.MapFrom(t => t.InitTime.ToString()))
                .ForMember(dt => dt.EndTime, opt => opt.MapFrom(t => t.EndTime.ToString()))
            .ReverseMap()
                .ForMember(t => t.InitTime, opt => opt.MapFrom(dt => TimeSpan.Parse(dt.InitTime)))
                .ForMember(t => t.EndTime, opt => opt.MapFrom(dt => TimeSpan.Parse(dt.EndTime)));

            CreateMap<Ticket, TicketCreateDto>()
                .ForMember(dt => dt.InitTime, opt => opt.MapFrom(t => t.InitTime.ToString()))
                .ForMember(dt => dt.EndTime, opt => opt.MapFrom(t => t.EndTime.ToString()))
            .ReverseMap()
                .ForMember(t => t.InitTime, opt => opt.MapFrom(dt => TimeSpan.Parse(dt.InitTime)))
                .ForMember(t => t.EndTime, opt => opt.MapFrom(dt => TimeSpan.Parse(dt.EndTime)));

            CreateMap<Ticket, TicketUpdateDto>()
                .ForMember(dt => dt.InitTime, opt => opt.MapFrom(t => t.InitTime.ToString()))
                .ForMember(dt => dt.EndTime, opt => opt.MapFrom(t => t.EndTime.ToString()))
            .ReverseMap()
                .ForMember(t => t.InitTime, opt => opt.MapFrom(dt => TimeSpan.Parse(dt.InitTime)))
                .ForMember(t => t.EndTime, opt => opt.MapFrom(dt => TimeSpan.Parse(dt.EndTime)));

            CreateMap<GbavsUser, Ticket>()
                .ForMember(t => t.OpUserName, opt => opt.MapFrom(u => u.Id))
                .ForMember(t => t.Id, opt => opt.Ignore())
            .ReverseMap()
                .ForMember(u => u.Id, opt => opt.MapFrom(t => t.OpUserName))
                .ForSourceMember(u => u.Id, opt => opt.DoNotValidate());

            CreateMap<GbavsUser, UserDto>()
                .ForMember(dto => dto.Terminal, op => op.MapFrom(gb => gb.TerminalId))
                .ReverseMap();

            CreateMap<GbavsUser, UserUpdateDto>().ReverseMap();
            CreateMap<GbavsUser, GbavsUser>().ReverseMap();

            CreateMap<UserDto, RegistrationDto>().ReverseMap();
            CreateMap<ReportRequestDto, ReportSummary>().ReverseMap();
            CreateMap<ReportSummaryDto, ReportSummary>().ReverseMap();
            CreateMap<ReportSummaryCreateDto, ReportSummary>().ReverseMap();
            CreateMap<ReportSummaryUpdateDto, ReportSummary>().ReverseMap();

            CreateMap<RegistrationDto, RegisterOperatorDto>().ReverseMap();
        }
    }
}
