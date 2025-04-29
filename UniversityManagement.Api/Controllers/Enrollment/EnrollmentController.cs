using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniversityManagement.Application.Features.Enrollments.Commands.CreateEnrollments;
using UniversityManagement.Application.Features.Enrollments.Commands.DeleteEnrollments;
using UniversityManagement.Application.Features.Enrollments.Commands.UpdateEnrollments;
using UniversityManagement.Application.Features.Enrollments.Quries.GetAllEnrollmentsList;
using UniversityManagement.Application.Features.Enrollments.Quries.GetEnrollmentsById;
using UniversityManagement.Shared.Resourses;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Api.Controllers.Enrollment
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateEnrollment([FromBody] CreateEnrollmentCommand command)
        {
            CommonResponse<int> result = await mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateEnrollment([FromBody] UpdateEnrollmentCommand command)
        {
            CommonResponse<int> result = await mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEnrollment([FromQuery] DeleteEnrollmentCommand command)
        {
            CommonResponse<int> result = await mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetEnrollmentById([FromQuery] GetEnrollmentByIdQuery request)
        {
            CommonResponse<GetEnrollmentByIdVm> result = await mediator.Send(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetPagedEnrollmentList")]
        public async Task<IActionResult> GetPagedEnrollmentList([FromQuery] GetEnrollmentListQuery request)
        {
            QueryResultResource<GetEnrollmentListVm> result = await mediator.Send(request);
            return Ok(result);
        }
    }
}
