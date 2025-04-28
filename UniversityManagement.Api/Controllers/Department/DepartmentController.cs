using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniversityManagement.Application.Features.Departments.Commands.CreateDepartments;
using UniversityManagement.Application.Features.Departments.Commands.DeleteDepartments;
using UniversityManagement.Application.Features.Departments.Commands.UpdateDepartments;
using UniversityManagement.Application.Features.Departments.Quries.GetAllDepartmentsList;
using UniversityManagement.Application.Features.Departments.Quries.GetDepartmentsById;
using UniversityManagement.Shared.Resourses;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Api.Controllers.Department
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentCommand command)
        {
            CommonResponse<int> result = await mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDepartment([FromBody] UpdateDepartmentCommand command)
        {
            CommonResponse<int> result = await mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDepartment([FromBody] DeleteDepartmentCommand command)
        {
            CommonResponse<int> result = await mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetDepartmentById([FromQuery] GetDepartmentByIdQuery request)
        {
            CommonResponse<GetDepartmentByIdVm> result = await mediator.Send(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetPagedDepartmentList")]
        public async Task<IActionResult> GetPagedDepartmentList([FromQuery] GetDepartmentListQuery request)
        {
            QueryResultResource<GetDepartmentListVm> result = await mediator.Send(request);
            return Ok(result);
        }
    }
}
