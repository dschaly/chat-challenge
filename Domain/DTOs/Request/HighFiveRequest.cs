using Domain.Resources;
using FluentValidation;

namespace Domain.DTOs.Request
{
    public sealed class HighFiveRequest
    {
        public int UserIdFrom { get; set; }
        public int UserIdTo { get; set; }
    }

    public class HighFiveRequestValidation : AbstractValidator<HighFiveRequest>
    {
        public HighFiveRequestValidation()
        {
            RuleFor(x => x.UserIdFrom).NotEmpty().WithMessage(string.Format(ValidationResource.Required, "Sending user"));
            RuleFor(x => x.UserIdTo).NotEmpty().WithMessage(string.Format(ValidationResource.Required, "Destination user"));
        }
    }
}
