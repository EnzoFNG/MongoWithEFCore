using MongoDB.Bson;
using MongoWithEF.Api.Application.Requests;
using MongoWithEF.Api.Application.Responses;

namespace MongoWithEF.Api.Application.Services;

public interface ICustomerService
{
    Task<IEnumerable<CustomerResponse>> GetAllAsync();
    Task<IEnumerable<CustomerResponse>> GetLikeNameAsync(string name);
    Task<CustomerResponse?> GetByIdAsync(ObjectId id);

    Task AddAsync(AddCustomerRequest customer);
    Task UpdateAsync(ObjectId id, UpdateCustomerRequest customer);
    Task DeleteAsync(ObjectId id);
}