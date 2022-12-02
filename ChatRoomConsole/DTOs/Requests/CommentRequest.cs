namespace ChatRoomConsole.DTOs.Requests
{
    public sealed class CommentRequest
    {
        public int UserId { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
