namespace Application.DTOs
{
    public sealed class ByMinuteActionResult
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int ActionId { get; set; }
        public DateTime ActionDate { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int UserIdTo { get; set; }
    }
}
