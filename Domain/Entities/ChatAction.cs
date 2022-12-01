using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public sealed class ChatAction : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
    }
}
