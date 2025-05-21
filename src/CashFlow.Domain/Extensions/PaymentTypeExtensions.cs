using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Extensions;
public static class PaymentTypeExtensions
{
    public static string PaymentTypeToString(this PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => ResourceFormatPaymentType.CASH,
            PaymentType.CreditCard => ResourceFormatPaymentType.CREDIT_CARD,
            PaymentType.DebitCard => ResourceFormatPaymentType.DEBIT_CARD,
            PaymentType.EletronicTransfer => ResourceFormatPaymentType.ELETRONIC_TRANSFER,
            _ => string.Empty
        };
    }
}
