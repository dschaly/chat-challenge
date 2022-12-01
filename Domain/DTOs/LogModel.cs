namespace Domain.DTOs
{
    public sealed class LogModel
    {
        public string SourceSystem { get; set; } = string.Empty;
        public string Application { get; set; } = string.Empty;
        public string Scenario { get; set; } = string.Empty;
        public string Identifier { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Information { get; set; } = string.Empty;
        public string Exception { get; set; } = string.Empty;
        public string Etc { get; set; } = string.Empty;
        public string CreationDate { get; set; } = string.Empty;
        public string UpdateDate { get; set; } = string.Empty;
        public string RunningTime { get; set; } = string.Empty;
    }
}
