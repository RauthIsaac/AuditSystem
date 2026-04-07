using AuditSystem.Application.Features.Course.Commands;
using AuditSystem.Application.Features.Enrollments.Commands;
using AuditSystem.Application.Features.Enrollments.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> CreateEnrollment([FromBody] CreateEnrollmentCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        /*------------------------------------------------------------------*/
        [HttpPut("UpdateEnrollmentStatus/{id}")]
        public async Task<IActionResult> UpdateEnrollmentPaymentStatus([FromRoute] Guid id, [FromQuery] bool isPaid)
        {
            var command = new UpdateEnrollmentPaymentStatusCommand(id, isPaid);

            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        /*------------------------------------------------------------------*/
        [HttpDelete("Delete/{id}")]
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
