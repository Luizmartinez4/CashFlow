using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAll;
public class GetAllExpensesUseCase : IGetAllExpensesUseCase
{
    private readonly IExpensesRepository _repository;

    public GetAllExpensesUseCase(IExpensesRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseExpensesJson> Execute()
    {
        var result = await _repository.GetAll();
    }
}
