using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCases.Expenses.Update;
public interface IUpdateExpensesUseCase
{
    Task Execute(long id, RequestExpenseJson request);
}
