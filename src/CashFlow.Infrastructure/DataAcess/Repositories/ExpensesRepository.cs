using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;
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

    public async Task<List<Expense>> GetAll()
    {
        return await _dbContent.Expenses.AsNoTracking().ToListAsync();
    }

    public async Task<Expense?> GetById(long id)
    {
        return await _dbContent.Expenses.AsNoTracking().FirstOrDefaultAsync
            (e => e.Id == id);
    }
}