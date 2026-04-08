using AuditSystem.Application.Features.Course.Commands;
using AuditSystem.Application.Features.Enrollments.Commands;
using AuditSystem.Application.Features.Enrollments.DTOs;
using AuditSystem.Application.Features.Enrollments.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuditSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        /*------------------------------------------------------------------*/
        private readonly IMediator _mediator;
        public EnrollmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /*------------------------------------------------------------------*/
        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllEnrollments()
        {
             var result = await _mediator.Send(new GetAllEnrollmentsQuery());
             if (!result.IsSuccess)
             {
                 return NotFound(result.Message);
             }
             return Ok(result);
        }
        /*------------------------------------------------------------------*/
        [HttpGet("GetEnrollmentByID/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEnrollmentById(Guid id)
        {
            var result = await _mediator.Send(new GetEnrollmentByIdQuery(id));
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result);
        }
        /*------------------------------------------------------------------*/
        [HttpPost("CreateEnrollment")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateEnrollment([FromBody] CreateEnrollmentRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            var command = new CreateEnrollmentCommand
            {
                UserId = Guid.Parse(userIdClaim),
                CourseId = request.CourseId
            };

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }
        /*------------------------------------------------------------------*/
        [HttpPut("UpdateEnrollmentStatus/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEnrollmentPaymentStatus([FromRoute] Guid id, [FromQuery] bool isPaid)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim);

            var command = new UpdateEnrollmentPaymentStatusCommand(id, isPaid, userId);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
        /*------------------------------------------------------------------*/
        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEnrollment(Guid id)
        {
            var result = await _mediator.Send(new DeleteEnrollmentCommand(id));
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result);
        }
        /*------------------------------------------------------------------*/
    }
}
