using System.Diagnostics.CodeAnalysis;

namespace Domain.DTOs.Response
{
    [ExcludeFromCodeCoverage]
    public sealed class HighFiveResponse
    {
        public int Id { get; set; }
        public int UserIdTo { get; set; }
    }
}
