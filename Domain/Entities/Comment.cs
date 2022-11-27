namespace Domain.Entities
{
    public sealed class Comment : BaseEntity
    {
        public int UserId { get; set; }
        public string Message { get; set; } = string.Empty;

        public User User { get; set; }
        public RoomAction RoomAction { get; set; }
    }
}
