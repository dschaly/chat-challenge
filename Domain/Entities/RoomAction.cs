namespace Domain.Entities
{
    public sealed class RoomAction : BaseEntity
    {
        public int UserId { get; set; }
        public int ActionId{ get; set; }
        public int CommentId{ get; set; }
        public int HighFiveId{ get; set; }
        public DateTime ActionDate { get; set; }
    }
}
