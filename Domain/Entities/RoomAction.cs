namespace Domain.Entities
{
    public sealed class RoomAction : BaseEntity
    {
        public int UserId { get; set; }
        public int ActionId{ get; set; }
        public int? CommentId{ get; set; }
        public int? HighFiveId{ get; set; }
        public DateTime ActionDate { get; set; }

        public User User { get; set; }
        public Comment? Comment { get; set; }
        public HighFive? HighFive { get; set; }
    }
}
