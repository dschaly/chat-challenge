using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseEntity<TPrimarykey>
    {
        public TPrimarykey Id { get; set; }
    }
}
