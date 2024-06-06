using MongoDB.Bson;
using MongoWithEF.Api.Domain.ValueObjects;

namespace MongoWithEF.Api.Domain.Entities;

public sealed class Customer
{
    private Customer()
    { }

    public Customer(ObjectId id, string name, string email, Address address)
    {
        Id = id;
        Name = name;
        Email = email;
        Address = address;
    }

    public ObjectId Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public Address Address { get; private set; } = default!;
}