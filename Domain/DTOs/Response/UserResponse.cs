using System.Diagnostics.CodeAnalysis;

namespace Domain.DTOs.Response
{
    [ExcludeFromCodeCoverage]
    public sealed class UserResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public bool IsOnline { get; set; }

        public List<HighFiveResponse> HighFives { get; set; }
    }
}
