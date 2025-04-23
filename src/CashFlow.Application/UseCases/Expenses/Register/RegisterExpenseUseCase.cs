using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCases : IRegisterExpenseUseCases
    {
        private readonly IExpensesRepository _repository;

        public RegisterExpenseUseCases(IExpensesRepository repository) 
        {
            _repository = repository;
        }

        public ResponseRegisteExpenseJson Execute(RequestExpenseJson request)
        {
            Validate(request);

            var entity = new Expense
            {
                amount = request.Amount,
                Date = request.Date,
                Description = request.Description,
                Title = request.Title,
                paymentType = (Domain.Enums.PaymentType)request.PaymentType
            };

            _repository.Add(entity);

            return new ResponseRegisteExpenseJson();
        }

        private void Validate(RequestExpenseJson request)
        {
            var validator = new RegisterExpenseValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }

    }

}
