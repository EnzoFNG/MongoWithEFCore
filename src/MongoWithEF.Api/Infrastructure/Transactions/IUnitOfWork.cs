namespace MongoWithEF.Api.Infrastructure.Transactions;

public interface IUnitOfWork
{
    Task CommitAsync();
}