using AutoMapper;
using UniversityManagement.Application.Features.Students.Commands.CreateStudents;
using UniversityManagement.Application.Features.Students.Commands.UpdateStudents;
using UniversityManagement.Application.Features.Students.Quries.GetAllStudentsList;
using UniversityManagement.Application.Features.Students.Quries.GetStudentById;
using UniversityManagement.Domain.Entities.Addresses;
using UniversityManagement.Domain.Entities.Students;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Application.Mapping
{
    public class StudentMapping : Profile
    {
        public StudentMapping()
        {
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<CreateStudentsCommand, Student>()
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
           {
               Street = src.Street,
               City = src.City,
               Country = src.Country,
               PostalCode = src.PostalCode
           }))
           .ForMember(dest => dest.Enrollments, opt => opt.Ignore());

            CreateMap<UpdateStudentsCommand, Student>()
          .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
          {
              Street = src.Street,
              City = src.City,
              Country = src.Country,
              PostalCode = src.PostalCode
          }))
          .ForMember(dest => dest.Enrollments, opt => opt.Ignore());

            CreateMap<GetStudentsByIdVm, Student>()
                   .ForMember(dest => dest.Enrollments, opt => opt.MapFrom(x => x.Enrollments))
                .ReverseMap();
            CreateMap<StudentQuery, GetStudentListQuery>().ReverseMap();
            CreateMap<GetStudentsListVm, Student>().ReverseMap();
        }
    }
}
