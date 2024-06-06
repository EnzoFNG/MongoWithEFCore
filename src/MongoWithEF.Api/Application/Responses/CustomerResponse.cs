using MongoWithEF.Api.Domain.ValueObjects;

namespace MongoWithEF.Api.Application.Responses;

public sealed record CustomerResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Address Address { get; set; } = default!;
}