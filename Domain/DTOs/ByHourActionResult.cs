namespace Domain.DTOs
{
    public sealed class ByHourActionResult
    {
        public int Id { get; set; }
        public DateTime HourPeriod { get; set; }
        public int EnteredPeopleCount { get; set; }
        public int LeftPeopleCount { get; set; }
        public int HighFivedFromPeopleCount { get; set; }
        public int HighFivedToPeopleCount { get; set; }
        public int CommentCount { get; set; }
    }
}
