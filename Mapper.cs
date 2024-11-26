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
            .ForMember(dest => dest.ServiceIds, opt => opt.MapFrom(src => src.AppointmentServices.Select(asj => asj.ServiceId)));

        CreateMap<CreateAppointmentDTO, Appointment>();
        CreateMap<UpdateAppointmentDTO, Appointment>();

        CreateMap<Customer, CustomerDTO>();
        CreateMap<CreateCustomerDTO, Customer>();

        CreateMap<Stylist, StylistDTO>()
            .ForMember(dest => dest.ServiceIds, opt => opt.MapFrom(src => src.StylistServices.Select(ssj => ssj.ServiceId)));

        CreateMap<CreateStylistDTO, Stylist>();

        CreateMap<Service, ServiceDTO>();
        CreateMap<CreateServiceDTO, Service>();
    }
}
