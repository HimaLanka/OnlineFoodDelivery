using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Services
{
    public interface IPaymentService
    {
        List<Payment> GetAllPayments();
        Payment GetPaymentById(int paymentId);
        decimal GetTotalAmountByMonth(int year, int month);
        PaymentResponse ProcessPayment(int orderId, string method, string? cardNumber, string? upiId);
        PaymentResponse UpdatePaymentMethod(int paymentId, string newMethod, string? cardNumber, string? upiId);
        string DeletePayment(int paymentId);
    }
}
