using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniversityManagement.Application.Features.Students.Commands.CreateStudents;
using UniversityManagement.Application.Features.Students.Commands.DeleteStudents;
using UniversityManagement.Application.Features.Students.Commands.UpdateStudents;
using UniversityManagement.Application.Features.Students.Quries.GetAllStudentsList;
using UniversityManagement.Application.Features.Students.Quries.GetStudentById;
using UniversityManagement.Shared.Resourses;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Api.Controllers.Student
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentsCommand command)
        {
            CommonResponse<int> result = await mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentsCommand command)
        {
            CommonResponse<int> result = await mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteStudent([FromQuery] DeleteStudentCommand command)
        {
            CommonResponse<int> result = await mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetStudentById([FromQuery] GetStudentByIdQuery request)
        {
            CommonResponse<GetStudentsByIdVm> result = await mediator.Send(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetPagedStudentList")]
        public async Task<IActionResult> GetPagedStudentList([FromQuery] GetStudentListQuery request)
        {
            QueryResultResource<GetStudentsListVm> result = await mediator.Send(request);
            return Ok(result);
        }
    }
}
