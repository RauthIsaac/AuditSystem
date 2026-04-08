using AuditSystem.Application.Features.Course.Commands;
using AuditSystem.Application.Features.Course.DTOs;
using AuditSystem.Application.Features.Course.Queries;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpGet("GetAll")]
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
        [HttpGet("GetCourseById/{id}")]
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
        [HttpPost("CreateCourse")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            var command = new CreateCourseCommand
            {
                UserId = Guid.Parse(userIdClaim),
                Title = request.Title,
                Description = request.Description,
                Author = request.Author,
                Price = request.Price
            };

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }
        /*------------------------------------------------------------------*/
        [HttpPut("UpdateCourse")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            var command = new UpdateCourseCommand
            {
                Id = request.Id,
                UserId = Guid.Parse(userIdClaim), 
                Title = request.Title,
                Description = request.Description,
                Author = request.Author,
                Price = request.Price
            };

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }
        /*------------------------------------------------------------------*/
        [HttpDelete("DeleteCourse/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            var result = await _mediator.Send(new DeleteCourseCommand(id, Guid.Parse(userIdClaim)));

            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(result);
        }
        /*------------------------------------------------------------------*/
    }
}
