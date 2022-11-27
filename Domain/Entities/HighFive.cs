namespace Domain.Entities
{
    public sealed class HighFive : BaseEntity
    {
        public int UserIdTo { get; set; }

        public User User { get; set; }
        public RoomAction RoomAction { get; set; } 
    }
}
