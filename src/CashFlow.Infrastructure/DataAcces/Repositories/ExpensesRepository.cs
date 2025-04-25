using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Infrastructure.DataAcces.Repositories;
internal class ExpensesRepository : IExpensesRepository
{
    private readonly CashFlowDbContext _dbContent;

    public ExpensesRepository(CashFlowDbContext dbContext)
    {
        _dbContent = dbContext;
    }

    public async Task Add(Expense expense)
    {
        await _dbContent.Expenses.AddAsync(expense);
    }
}
