using MongoWithEF.Api.Domain.Entities;
using System.Linq.Expressions;

namespace MongoWithEF.Api.Domain.Interfaces;

public interface ICustomerRepository
{
    ValueTask<IEnumerable<Customer>> GetAllAsync();
    ValueTask<IEnumerable<Customer>> GetWhereAsync(Expression<Func<Customer, bool>> condition);
    ValueTask<Customer?> GetOneWhereAsync(Expression<Func<Customer, bool>> condition);
    ValueTask<bool> ExistsAsync(Expression<Func<Customer, bool>> condition);

    Task AddAsync(Customer customer);
    void Update(Customer customer);
    void Delete(Customer customer);
}