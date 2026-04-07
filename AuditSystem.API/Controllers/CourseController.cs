using AuditSystem.Application.Features.Course.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuditSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        /*------------------------------------------------------------------*/
        private readonly IMediator _mediator;
        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /*------------------------------------------------------------------*/
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var result = await _mediator.Send(new GetAllCoursesQuery());
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result);
        }
        /*------------------------------------------------------------------*/
        [HttpGet("id")]
        public async Task<IActionResult> GetCourseById(Guid id)
        {
            var result = await _mediator.Send(new GetCourceByIdQuery(id));
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result);
        }
        /*------------------------------------------------------------------*/

    }
}
