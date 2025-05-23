﻿using AutoMapper;
using UniversityManagement.Application.Features.Courses.Commands.CreateCourses;
using UniversityManagement.Application.Features.Courses.Commands.UpdateCourses;
using UniversityManagement.Application.Features.Courses.Quries.GetAllCoursesList;
using UniversityManagement.Application.Features.Courses.Quries.GetCoursesById;
using UniversityManagement.Application.Features.Registration.Commands.CreateRegistrations;
using UniversityManagement.Application.Features.Registration.Quries.GetAllUsersList;
using UniversityManagement.Domain.Entities.Courses;
using UniversityManagement.Shared.Resourses;

namespace UniversityManagement.Application.Mapping
{
    public class CoruseMapping : Profile
    {
        public CoruseMapping()
        {
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<CreateCourseCommand, Course>()
           .ForMember(dest => dest.Enrollments, opt => opt.Ignore());

            CreateMap<UpdateCourseCommand, Course>()
          .ForMember(dest => dest.Enrollments, opt => opt.Ignore());

            CreateMap<GetCourseByIdVm, Course>()
                     .ForMember(dest => dest.Department, opt => opt.MapFrom(x => x.Department))
                .ReverseMap();
            CreateMap<CourseQuery, GetCourseListQuery>().ReverseMap();
            CreateMap<GetCourseListVm, Course>().ReverseMap();
        }
    }
}
