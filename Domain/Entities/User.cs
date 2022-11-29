namespace Domain.Entities
{
    public sealed class User : BaseEntity<int>
    {
        public User()
        {
            RoomActions = new List<RoomAction>();
            Comments = new List<Comment>();
            HighFives = new List<HighFive>();
        }

        public string UserName { get; set; }

        public ICollection<RoomAction> RoomActions { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<HighFive> HighFives { get; set; }
    }
}
