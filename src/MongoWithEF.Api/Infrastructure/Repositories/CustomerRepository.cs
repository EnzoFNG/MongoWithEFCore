using Microsoft.EntityFrameworkCore;
using MongoWithEF.Api.Domain.Entities;
using MongoWithEF.Api.Domain.Interfaces;
using MongoWithEF.Api.Infrastructure.Context;
using System.Linq.Expressions;

namespace MongoWithEF.Api.Infrastructure.Repositories;

public sealed class CustomerRepository(CustomersDbContext customersDbContext) : ICustomerRepository
{
    private readonly DbSet<Customer> customers = customersDbContext.Set<Customer>();

    public async ValueTask<IEnumerable<Customer>> GetAllAsync()
    {
        return await customers.ToListAsync();
    }

    public async ValueTask<Customer?> GetOneWhereAsync(Expression<Func<Customer, bool>> condition)
    {
        return await customers.FirstOrDefaultAsync(condition);
    }

    public async ValueTask<IEnumerable<Customer>> GetWhereAsync(Expression<Func<Customer, bool>> condition)
    {
        return await customers.Where(condition).ToListAsync();
    }

    public async ValueTask<bool> ExistsAsync(Expression<Func<Customer, bool>> condition)
    {
        return await customers.AnyAsync(condition);
    }

    public async Task AddAsync(Customer customer)
    {
        await customers.AddAsync(customer);
    }

    public void Update(Customer customer)
    {
        customers.Update(customer);
    }

    public void Delete(Customer customer)
    {
        customers.Remove(customer);
    }
}