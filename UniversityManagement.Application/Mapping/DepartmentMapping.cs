using AutoMapper;
using UniversityManagement.Application.Features.Courses.Quries.GetCoursesById;
using UniversityManagement.Application.Features.Departments.Commands.CreateDepartments;
using UniversityManagement.Application.Features.Departments.Commands.UpdateDepartments;
using UniversityManagement.Application.Features.Departments.Quries.GetAllDepartmentsList;
using UniversityManagement.Application.Features.Departments.Quries.GetDepartmentsById;
using UniversityManagement.Domain.Entities.Departments;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Application.Mapping
{
    public class DepartmentMapping : Profile
    {
        public DepartmentMapping()
        {
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<CreateDepartmentCommand, Department>();

            CreateMap<UpdateDepartmentCommand, Department>();

            CreateMap<GetDepartmentByIdQuery, Department>().ReverseMap();
            CreateMap<DepartmentQuery, GetDepartmentListQuery>().ReverseMap();
            CreateMap<GetDepartmentListVm, Department>().ReverseMap();
            CreateMap<DepartmentResponseVm, Department>().ReverseMap();
        }
    }
}
