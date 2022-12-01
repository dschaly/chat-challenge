namespace Domain.Entities
{
    public sealed class HighFive : BaseEntity<int>
    {
        public int UserIdTo { get; set; }
        public string UserNameTo { get; set; } = string.Empty;

        public User User { get; set; }
        public RoomAction RoomAction { get; set; } 
    }
}
