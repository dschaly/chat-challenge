namespace Domain.DTOs.Response
{
    public sealed class RoomActionResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ActionId { get; set; }
        public int? CommentId { get; set; }
        public int? HighFiveId { get; set; }
        public DateTime ActionDate { get; set; }

        public UserResponse User { get; set; }
        public CommentResponse? Comment { get; set; }
        public HighFiveResponse? HighFive { get; set; }
    }
}
