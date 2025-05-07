using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.GetById;
public interface IGetExpenseByidUseCase
{
    Task<ResponseExpenseJson> Execute(long id);
}
