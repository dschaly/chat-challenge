using System.Diagnostics.CodeAnalysis;

namespace Domain.DTOs.Response
{
    [ExcludeFromCodeCoverage]
    public sealed class CommentResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; } = string.Empty;

        //public UserResponse User { get; set; }
        //public RoomActionResponse RoomAction { get; set; }
    }
}
