using AutoMapper;
using UserAlertManagement.Data.Models;
using UserAlertManagement.Services.Models;

namespace UserAlertManagement.Services.Mappers;

public class AlertMapper : Profile
{
    public AlertMapper()
    {
        CreateMap<Alert, Alert>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.UserId, opt => opt.Ignore())
            .ForMember(d => d.FromAirport, opt => opt.MapFrom(s => s.FromAirport))
            .ForMember(d => d.ToAirport, opt => opt.MapFrom(s => s.ToAirport))
            .ForMember(d => d.MaxPrice, opt => opt.MapFrom(s => s.MaxPrice))
            .ForMember(d => d.DepartureDate, opt => opt.MapFrom(s => s.DepartureDate))
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())
            .ForMember(d => d.IsActive, opt => opt.MapFrom(s => s.IsActive))
            .ForMember(d => d.IsOn, opt => opt.MapFrom(s => s.IsOn));
        
        CreateMap<Alert, AlertModel>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.UserId))
            .ForMember(d => d.FromAirport, opt => opt.MapFrom(s => s.FromAirport))
            .ForMember(d => d.ToAirport, opt => opt.MapFrom(s => s.ToAirport))
            .ForMember(d => d.MaxPrice, opt => opt.MapFrom(s => s.MaxPrice))
            .ForMember(d => d.DepartureDate, opt => opt.MapFrom(s => s.DepartureDate))
            .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt))
            .ForMember(d => d.IsActive, opt => opt.MapFrom(s => s.IsActive))
            .ForMember(d => d.IsOn, opt => opt.MapFrom(s => s.IsOn));

        CreateMap<AlertModel, Alert>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.UserId))
            .ForMember(d => d.FromAirport, opt => opt.MapFrom(s => s.FromAirport))
            .ForMember(d => d.ToAirport, opt => opt.MapFrom(s => s.ToAirport))
            .ForMember(d => d.MaxPrice, opt => opt.MapFrom(s => s.MaxPrice))
            .ForMember(d => d.DepartureDate, opt => opt.MapFrom(s => s.DepartureDate))
            .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt))
            .ForMember(d => d.IsActive, opt => opt.MapFrom(s => s.IsActive))
            .ForMember(d => d.IsOn, opt => opt.MapFrom(s => s.IsOn))
            .ForMember(d => d.User, opt => opt.Ignore());
    }
}