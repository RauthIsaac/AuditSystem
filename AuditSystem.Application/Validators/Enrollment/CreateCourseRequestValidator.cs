using AuditSystem.Application.Features.Course.DTOs;
using FluentValidation;

namespace AuditSystem.Application.Validators.Course
{
    public class CreateCourseRequestValidator : AbstractValidator<CreateCourseRequest>
    {
        public CreateCourseRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MinimumLength(10).WithMessage("Description must be at least 10 characters");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Author is required");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}