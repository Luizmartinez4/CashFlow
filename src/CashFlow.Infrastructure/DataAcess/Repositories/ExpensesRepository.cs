using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;
internal class ExpensesRepository : IExpensesReadOnlyRepository, IExpensesWriteOnlyRepository, IExpensesUpdateOnlyRepository
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

    public async Task<bool> Delete(long id)
    {
        var result = await _dbContent.Expenses.FirstOrDefaultAsync
            (e => e.Id == id);
        
        if(result is null)
        {
            return false;
        }
        _dbContent.Expenses.Remove(result);

        return true;
    }

    public async Task<List<Expense>> GetAll()
    {
        return await _dbContent.Expenses.AsNoTracking().ToListAsync();
    }

    async Task<Expense?> IExpensesReadOnlyRepository.GetById(long id)
    {
        return await _dbContent.Expenses.AsNoTracking().FirstOrDefaultAsync
            (e => e.Id == id);
    }

    async Task<Expense?> IExpensesUpdateOnlyRepository.GetById(long id)
    {
        return await _dbContent.Expenses.FirstOrDefaultAsync
            (e => e.Id == id);
    }

    public void Update(Expense expense)
    {
        _dbContent.Expenses.Update(expense);
    }

    public async Task<List<Expense>> FilterByMonth(DateOnly date)
    {
        var startDate = new DateTime(date.Year, date.Month, day: 1).Date;

        var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
        var endDate = new DateTime(date.Year, date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);

        return await _dbContent
            .Expenses
            .AsNoTracking()
            .Where(e => e.Date >= startDate && e.Date <= endDate)
            .OrderBy(e => e.Date)
            .ToListAsync();
    }
}