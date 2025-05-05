using AutoMapper;
using UniversityManagement.Application.Features.Registration.Commands.CreateRegistrations;
using UniversityManagement.Domain.Entities.Users;

namespace UniversityManagement.Application.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, CreateRegistrationCommand>().ReverseMap();
        }
    }
}
