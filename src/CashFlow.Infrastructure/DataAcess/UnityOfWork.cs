using CashFlow.Domain.Repositories;

namespace CashFlow.Infrastructure.DataAccess;
internal class UnitOfWork : IUnitOfWork
{
    private readonly CashFlowDbContext _dbContent;

    public UnitOfWork(CashFlowDbContext dbContext)
    {
        _dbContent = dbContext;
    }

    public async Task Commit() => await _dbContent.SaveChangesAsync();
}