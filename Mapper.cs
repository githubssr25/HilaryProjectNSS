using Models;
using Models.DTOs;
using AutoMapper;
namespace Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Appointment, AppointmentDTO>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
            .ForMember(dest => dest.StylistName, opt => opt.MapFrom(src => src.Stylist.Name))
            .ForMember(dest => dest.ServiceIds, opt => opt.MapFrom(src => src.AppointmentServiceJoinList.Select(asj => asj.ServiceId)));

        CreateMap<CreateAppointmentDTO, Appointment>();
        CreateMap<UpdateAppointmentDTO, Appointment>();

        CreateMap<Customer, CustomerDTO>()
        .ForMember(dest => dest.AppointmentIds, opt => opt.MapFrom(src => src.Appointments.Select(a => a.AppointmentId)));

        CreateMap<CreateCustomerDTO, Customer>();

        CreateMap<Stylist, StylistDTO>()
            .ForMember(dest => dest.ServiceIds, opt => opt.MapFrom(src => src.StylistServiceJoinList.Select(ssj => ssj.ServiceId)));

        CreateMap<CreateStylistDTO, Stylist>();

        CreateMap<Service, ServiceDTO>();
        CreateMap<CreateServiceDTO, Service>();
    }
}
