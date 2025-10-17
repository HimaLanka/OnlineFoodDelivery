using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Repository
{
    public interface IPaymentRepository
    {

        List<Payment> GetAllPayments();
        Payment GetPaymentById(int paymentId);
        decimal GetTotalAmountByMonth(int year, int month);
        PaymentResponse ProcessCardPayment(int orderId, string cardNumber);
        PaymentResponse ProcessUPIPayment(int orderId, string upiId);
        PaymentResponse ProcessCODPayment(int orderId);
        PaymentResponse UpdatePaymentMethod(int paymentId, string newMethod, string? cardNumber, string? upiId);
        void DeletePayment(Payment payment);

    }
}
