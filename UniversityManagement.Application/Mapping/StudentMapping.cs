using AutoMapper;
using UniversityManagement.Application.Features.Courses.Commands.CreateCourses;
using UniversityManagement.Application.Features.Courses.Commands.UpdateCourses;
using UniversityManagement.Application.Features.Courses.Quries.GetAllCoursesList;
using UniversityManagement.Application.Features.Courses.Quries.GetCoursesById;
using UniversityManagement.Application.Features.Departments.Commands.CreateDepartments;
using UniversityManagement.Application.Features.Departments.Commands.UpdateDepartments;
using UniversityManagement.Application.Features.Departments.Quries.GetAllDepartmentsList;
using UniversityManagement.Application.Features.Departments.Quries.GetDepartmentsById;
using UniversityManagement.Application.Features.Enrollments.Commands.CreateEnrollments;
using UniversityManagement.Application.Features.Enrollments.Commands.UpdateEnrollments;
using UniversityManagement.Application.Features.Enrollments.Quries.GetAllEnrollmentsList;
using UniversityManagement.Application.Features.Enrollments.Quries.GetEnrollmentsById;
using UniversityManagement.Application.Features.Instructors.Commands.CreateInstructors;
using UniversityManagement.Application.Features.Instructors.Commands.UpdateInstructors;
using UniversityManagement.Application.Features.Instructors.Quries.GetAllInstructorsList;
using UniversityManagement.Application.Features.Instructors.Quries.GetInstructorsById;
using UniversityManagement.Application.Features.Students.Commands.CreateStudents;
using UniversityManagement.Application.Features.Students.Commands.UpdateStudents;
using UniversityManagement.Application.Features.Students.Quries.GetAllStudentsList;
using UniversityManagement.Application.Features.Students.Quries.GetStudentById;
using UniversityManagement.Domain.Entities.Addresses;
using UniversityManagement.Domain.Entities.Courses;
using UniversityManagement.Domain.Entities.Departments;
using UniversityManagement.Domain.Entities.Enrollments;
using UniversityManagement.Domain.Entities.Instructors;
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

            CreateMap<GetStudentsByIdVm, Student>().ReverseMap();
            CreateMap<StudentQuery, GetStudentListQuery>().ReverseMap();
            CreateMap<GetStudentsListVm, Student>().ReverseMap();
        }
    }
}
