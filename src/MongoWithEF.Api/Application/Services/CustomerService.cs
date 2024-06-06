using MongoDB.Bson;
using MongoWithEF.Api.Application.Requests;
using MongoWithEF.Api.Application.Responses;
using MongoWithEF.Api.Domain.Entities;
using MongoWithEF.Api.Domain.Interfaces;
using MongoWithEF.Api.Infrastructure.Transactions;

namespace MongoWithEF.Api.Application.Services;

public sealed class CustomerService(IUnitOfWork unitOfWork, ICustomerRepository customerRepository) : ICustomerService
{
    public async Task AddAsync(AddCustomerRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new Exception("Nome inválido");

        if (string.IsNullOrWhiteSpace(request.Email))
            throw new Exception("Email inválido");

        var customerExists = await customerRepository.ExistsAsync(x => x.Email == request.Email);

        if (customerExists)
            throw new Exception("Um cliente com esse email já existe");

        var customer = new Customer(ObjectId.GenerateNewId(), request.Name, request.Email, request.Address);

        try
        {
            await customerRepository.AddAsync(customer);
            await unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task UpdateAsync(ObjectId id, UpdateCustomerRequest request)
    {
        if (id == ObjectId.Empty)
            throw new Exception("Código inválido");

        if (string.IsNullOrWhiteSpace(request.Name))
            throw new Exception("Nome inválido");

        if (string.IsNullOrWhiteSpace(request.Email))
            throw new Exception("Email inválido");

        var customerExists = await customerRepository.ExistsAsync(x => x.Email == request.Email && x.Id != id);

        if (customerExists)
            throw new Exception("Um cliente com esse email já existe");

        var existentCustomer = await customerRepository.GetOneWhereAsync(x => x.Id == id)
            ?? throw new Exception("O cliente especificado não existe");

        var customer = new Customer(existentCustomer.Id, request.Name, request.Email, request.Address);

        try
        {
            customerRepository.Update(customer);
            await unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task DeleteAsync(ObjectId id)
    {
        if (id == ObjectId.Empty)
            throw new Exception("Código inválido");

        var existentCustomer = await customerRepository.GetOneWhereAsync(x => x.Id == id)
            ?? throw new Exception("O cliente especificado não existe");

        try
        {
            customerRepository.Delete(existentCustomer);
            await unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<CustomerResponse>> GetAllAsync()
    {
        var customers = await customerRepository.GetAllAsync();

        var response = customers.Select(x => new CustomerResponse
        {
            Id = x.Id.ToString(),
            Name = x.Name,
            Email = x.Email,
            Address = x.Address
        });

        return response;
    }

    public async Task<CustomerResponse?> GetByIdAsync(ObjectId id)
    {
        if (id == ObjectId.Empty)
            throw new ArgumentException("Código inválido");

        var customer = await customerRepository.GetOneWhereAsync(x => x.Id == id)
            ?? throw new InvalidDataException("O cliente especificado não existe");

        var response = new CustomerResponse
        {
            Id = customer.Id.ToString(),
            Name = customer.Name,
            Email = customer.Email,
            Address = customer.Address
        };

        return response;
    }

    public async Task<IEnumerable<CustomerResponse>> GetLikeNameAsync(string name)
    {
        var customers = await customerRepository.GetWhereAsync(x => x.Name.Contains(name));

        var response = customers.Select(x => new CustomerResponse
        {
            Id = x.Id.ToString(),
            Name = x.Name,
            Email = x.Email,
            Address = x.Address
        });

        return response;
    }
}