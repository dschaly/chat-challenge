using Domain.Resources;
using FluentValidation;

namespace Domain.DTOs.Request
{
    public sealed class CommentRequest
    {
        public int UserId { get; set; }
        public string Comment { get; set; } = string.Empty;
    }

    public class CommentRequestValidation : AbstractValidator<CommentRequest>
    {
        public CommentRequestValidation()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage(string.Format(ValidationResource.Required, "User Id"));
            RuleFor(x => x.Comment).NotEmpty().WithMessage(string.Format(ValidationResource.Required, "Comment"));
        }
    }
}
