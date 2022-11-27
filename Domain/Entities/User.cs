namespace Domain.Entities
{
    public sealed class User : BaseEntity
    {
        //public User()
        //{
        //    RoomActions = new List<RoomAction>();
        //    Comments = new List<Comment>();
        //    HighFives = new List<HighFive>();
        //}

        public string UserName { get; set; } = string.Empty;

        public List<RoomAction> RoomActions { get; set; }
        public List<Comment> Comments { get; set; }
        public List<HighFive> HighFives { get; set; }
    }
}
