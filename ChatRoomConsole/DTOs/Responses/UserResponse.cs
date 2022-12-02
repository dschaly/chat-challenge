namespace ChatRoomConsole.DTOs.Responses
{
    public sealed class UserResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public bool IsOnline { get; set; }
    }
}
