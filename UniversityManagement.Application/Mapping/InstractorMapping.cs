using AutoMapper;
using UniversityManagement.Application.Features.Instructors.Commands.CreateInstructors;
using UniversityManagement.Application.Features.Instructors.Commands.UpdateInstructors;
using UniversityManagement.Application.Features.Instructors.Quries.GetAllInstructorsList;
using UniversityManagement.Application.Features.Instructors.Quries.GetInstructorsById;
using UniversityManagement.Domain.Entities.Instructors;
using UniversityManagement.Domain.Entities.OfficeAssignments;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Application.Mapping
{
    public class InstractorMapping : Profile
    {
        public InstractorMapping()
        {
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<CreateInstructorCommand, Instructor>()
          .ForMember(dest => dest.OfficeAssignment, opt => opt.MapFrom(src => new OfficeAssignment
          {
              Location = src.Location
          }));

            CreateMap<UpdateInstructorCommand, Instructor>()
          .ForMember(dest => dest.OfficeAssignment, opt => opt.MapFrom(src => new OfficeAssignment
          {
              Location = src.Location
          }));

            CreateMap<GetInstructorByIdVm, Instructor>().ReverseMap();
            CreateMap<InstructorQuery, GetInstructorListQuery>().ReverseMap();
            CreateMap<GetInstructorListVm, Instructor>().ReverseMap();
        }
    }
}
