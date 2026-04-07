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
        [HttpGet]
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
        [HttpGet("id")]
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
    }
}
