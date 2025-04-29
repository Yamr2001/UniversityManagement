using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniversityManagement.Application.Features.Courses.Commands.CreateCourses;
using UniversityManagement.Application.Features.Courses.Commands.DeleteCourses;
using UniversityManagement.Application.Features.Courses.Commands.UpdateCourses;
using UniversityManagement.Application.Features.Courses.Quries.GetAllCoursesList;
using UniversityManagement.Application.Features.Courses.Quries.GetCoursesById;
using UniversityManagement.Shared.Resourses;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Api.Controllers.Courses
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand command)
        {
            CommonResponse<int> result = await mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseCommand command)
        {
            CommonResponse<int> result = await mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCourse([FromQuery] DeleteCourseCommand command)
        {
            CommonResponse<int> result = await mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetCourseById([FromQuery] GetCourseByIdQuery request)
        {
            CommonResponse<GetCourseByIdVm> result = await mediator.Send(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetPagedCourseList")]
        public async Task<IActionResult> GetPagedCourseList([FromQuery] GetCourseListQuery request)
        {
            QueryResultResource<GetCourseListVm> result = await mediator.Send(request);
            return Ok(result);
        }
    }
}
