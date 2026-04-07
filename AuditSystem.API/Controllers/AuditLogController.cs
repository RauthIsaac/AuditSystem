using AuditSystem.Application.Features.AuditLog.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuditSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        /*------------------------------------------------------------------*/
        private readonly IMediator _mediator;
        public AuditLogController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /*------------------------------------------------------------------*/
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAuditLogs()
        {
            var result = await _mediator.Send(new GetAllAuditsLogQuery());
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result);
        }
        /*------------------------------------------------------------------*/
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetAuditLogById(Guid id)
        {
            var result = await _mediator.Send(new GetAuditLogByIdQuery(id));
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result);
        }
        /*------------------------------------------------------------------*/

    }
}
