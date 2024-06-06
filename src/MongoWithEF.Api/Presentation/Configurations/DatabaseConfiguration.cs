using Bogus;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoWithEF.Api.Domain.Entities;
using MongoWithEF.Api.Domain.ValueObjects;
using MongoWithEF.Api.Infrastructure.Context;

namespace MongoWithEF.Api.Presentation.Configurations;

public static class DatabaseConfiguration
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        var connectionString = configuration.GetConnectionString("MongoDB");

        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        services.AddDbContext<CustomersDbContext>(options =>
        {
            var mongoClient = new MongoClient(connectionString);
            options.UseMongoDB(mongoClient, "CustomersDB");
        });

        services.AddScoped<CustomersDbContext>();

        SeedData(services);

        return services;
    }

    private static void SeedData(IServiceCollection services)
    {
        var faker = new Faker<Customer>("pt_BR").CustomInstantiator(f =>
        {
            var addressFaker = new Faker<Address>("pt_BR")
            .CustomInstantiator(f => new Address(
                f.Address.StreetName(),
                f.Address.BuildingNumber(),
                f.Address.StreetSuffix(),
                f.Address.City(),
                f.Address.StateAbbr(),
                f.Address.ZipCode(),
                f.Address.SecondaryAddress()));

            var address = addressFaker.Generate(1).First();

            return new Customer(ObjectId.GenerateNewId(), f.Person.FullName, f.Internet.Email(), address);
        });

        var customers = faker.Generate(30);

        var serviceProvider = services.BuildServiceProvider();

        var dbContext = serviceProvider.GetRequiredService<CustomersDbContext>();

        var dataExists = dbContext.Customers.Any();

        if (!dataExists)
        {
            dbContext.Customers.AddRange(customers);
            dbContext.SaveChanges();
        }
    }
}