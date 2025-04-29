using AutoMapper;
using UniversityManagement.Application.Features.Enrollments.Commands.CreateEnrollments;
using UniversityManagement.Application.Features.Enrollments.Commands.UpdateEnrollments;
using UniversityManagement.Application.Features.Enrollments.Quries.GetAllEnrollmentsList;
using UniversityManagement.Application.Features.Enrollments.Quries.GetEnrollmentsById;
using UniversityManagement.Application.Features.Students.Quries.GetStudentById;
using UniversityManagement.Domain.Entities.Enrollments;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Application.Mapping
{
    public class EnrollmentMapping : Profile
    {
        public EnrollmentMapping()
        {
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<CreateEnrollmentCommand, Enrollment>();

            CreateMap<UpdateEnrollmentCommand, Enrollment>();

            CreateMap<GetEnrollmentByIdVm, Enrollment>().ReverseMap();
            CreateMap<EnrollmentQuery, GetEnrollmentListQuery>().ReverseMap();
            CreateMap<GetEnrollmentListVm, Enrollment>().ReverseMap();
            CreateMap<EnrollmentResponseVm, Enrollment>().ReverseMap();
        }
    }
}
