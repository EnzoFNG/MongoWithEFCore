namespace MongoWithEF.Api.Domain.ValueObjects;

public sealed class Address
{
    private Address()
    { }

    public Address(
       string street,
       string number,
       string neighborhood,
       string city,
       string state,
       string zipCode,
       string? complement = null)
    {
        Street = street;
        Number = number;
        Complement = complement;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

    public string ZipCode { get; private set; } = string.Empty;
    public string Street { get; private set; } = string.Empty;
    public string Number { get; private set; } = string.Empty;
    public string? Complement { get; private set; } = string.Empty;
    public string Neighborhood { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string State { get; private set; } = string.Empty;

    public override string ToString()
    {
        return Complement is null
            ? $"{Street}, {Number} - {Neighborhood}, {City} - {State}"
            : $"{Street}, {Number}, {Complement} - {Neighborhood}, {City} - {State}";
    }
}