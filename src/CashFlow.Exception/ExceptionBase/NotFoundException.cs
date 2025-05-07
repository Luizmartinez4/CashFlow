namespace CashFlow.Exception.ExceptionBase;
public class NotFoundException : CashFlowException
{
    public string Error = string.Empty;
    public NotFoundException(string errorMessage)
    {
        Error = errorMessage;
    }
}
