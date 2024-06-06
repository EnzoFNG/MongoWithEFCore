
using MongoWithEF.Api.Infrastructure.Context;

namespace MongoWithEF.Api.Infrastructure.Transactions;

public sealed class UnitOfWork(CustomersDbContext dbContext) : IUnitOfWork
{

    public async Task CommitAsync()
    {
        if (await dbContext.SaveChangesAsync() <= 0)
            throw new InvalidOperationException("Erro ao salvar os dados.");
    }
}