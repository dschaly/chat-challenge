namespace Domain.Entities
{
    public sealed class ChatAction : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
    }
}
