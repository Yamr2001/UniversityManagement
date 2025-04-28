using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniversityManagement.Application.Features.Instructors.Commands.CreateInstructors;
using UniversityManagement.Application.Features.Instructors.Commands.DeleteInstructors;
using UniversityManagement.Application.Features.Instructors.Commands.UpdateInstructors;
using UniversityManagement.Application.Features.Instructors.Quries.GetAllInstructorsList;
using UniversityManagement.Application.Features.Instructors.Quries.GetInstructorsById;
using UniversityManagement.Shared.Resourses;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Api.Controllers.Instractor
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstractorController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateInstractor([FromBody] CreateInstructorCommand command)
        {
            CommonResponse<int> result = await mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateInstractor([FromBody] UpdateInstructorCommand command)
        {
            CommonResponse<int> result = await mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteInstractor([FromBody] DeleteInstructorCommand command)
        {
            CommonResponse<int> result = await mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetInstractorById([FromQuery] GetInstructorByIdQuery request)
        {
            CommonResponse<GetInstructorByIdVm> result = await mediator.Send(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetPagedInstractorList")]
        public async Task<IActionResult> GetPagedInstractorList([FromQuery] GetInstructorListQuery request)
        {
            QueryResultResource<GetInstructorListVm> result = await mediator.Send(request);
            return Ok(result);
        }
    }
}
