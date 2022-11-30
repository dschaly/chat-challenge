using Domain.Resources;
using FluentValidation;

namespace Domain.DTOs.Request
{
    public sealed class LeaveTheRoomRequest
    {
        public int UserId { get; set; }
    }

    public class LeaveTheRoomRequestValidation : AbstractValidator<LeaveTheRoomRequest>
    {
        public LeaveTheRoomRequestValidation()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage(string.Format(ValidationResource.Required, "User Id"));
        }
    }
}
