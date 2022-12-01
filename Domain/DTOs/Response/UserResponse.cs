namespace Domain.DTOs.Response
{
    public sealed class UserResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public bool IsOnline { get; set; }

        //public List<RoomActionResponse> RoomActions { get; set; }
        //public List<CommentResponse> Comments { get; set; }
        public List<HighFiveResponse> HighFives { get; set; }
    }
}
