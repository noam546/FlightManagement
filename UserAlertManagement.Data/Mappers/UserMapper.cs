using AutoMapper;
using UserAlertManagement.Data.Models;

namespace UserAlertManagement.Data.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, User>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.DateOfBirth, opt => opt.MapFrom(s => s.DateOfBirth))
            .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
            .ForMember(d => d.DeviceToken, opt => opt.MapFrom(s => s.DeviceToken))
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())
            .ForMember(d => d.LastUpdatedAt, opt => opt.Ignore())
            .ForMember(d => d.LastLogin, opt => opt.Ignore())
            .ForMember(d => d.Alerts, opt => opt.Ignore());
    }
}