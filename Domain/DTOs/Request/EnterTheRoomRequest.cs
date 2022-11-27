using Domain.Resources;
using FluentValidation;

namespace Domain.DTOs.Request
{
    public sealed class EnterTheRoomRequest
    {
        public string UserName { get; set; }
    }

    public class EnterTheRoomRequestValidation : AbstractValidator<EnterTheRoomRequest>
    {
        public EnterTheRoomRequestValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage(string.Format(ValidationResource.Required, "User Name"));
        }
    }
}
