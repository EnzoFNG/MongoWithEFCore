using MongoWithEF.Api.Domain.ValueObjects;

namespace MongoWithEF.Api.Application.Requests;

public sealed record UpdateCustomerRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Address Address { get; set; } = default!;
}