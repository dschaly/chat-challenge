namespace Domain.Entities
{
    public sealed class HighFive : BaseEntity<int>
    {
        public int UserIdTo { get; set; }

        public User User { get; set; }
        public RoomAction RoomAction { get; set; } 
    }
}
