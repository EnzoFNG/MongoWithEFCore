using MongoWithEF.Api.Application.Services;
using MongoWithEF.Api.Domain.Interfaces;
using MongoWithEF.Api.Infrastructure.Repositories;
using MongoWithEF.Api.Infrastructure.Transactions;

namespace MongoWithEF.Api.Presentation.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ICustomerService, CustomerService>();

        return services;
    }
}