namespace Application.Mapper.Response
{
    public sealed class ByHourActionResult
    {
        public DateTime HourPeriod { get; set; }
        public int EnteredPeopleCount { get; set; }
        public int LeftPeopleCount { get; set; }
        public int HighFivedFromPeopleCount { get; set; }
        public int HighFivedToPeopleCount { get; set; }
        public int CommentCount { get; set; }
    }
}
