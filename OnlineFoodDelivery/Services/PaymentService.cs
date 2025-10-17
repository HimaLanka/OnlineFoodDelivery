using OnlineFoodDelivery.Exceptions;
using OnlineFoodDelivery.Model;
using OnlineFoodDelivery.Repository;
using System.Text.RegularExpressions;

namespace OnlineFoodDelivery.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public List<Payment> GetAllPayments()
        {
            try
            {
                return _paymentRepository.GetAllPayments();
            }
            catch (Exception ex)
            {
                throw new PaymentProcessingException("Failed to retrieve payments.");
            }
        }

        public Payment GetPaymentById(int paymentId)
        {
            try
            {
                var payment = _paymentRepository.GetPaymentById(paymentId);
                if (payment == null)
                    throw new OrderNotExistsException(paymentId);

                return payment;
            }
            catch (OrderNotExistsException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new PaymentProcessingException("Failed to retrieve payment.");
            }
        }

        public decimal GetTotalAmountByMonth(int year, int month)
        {
            try
            {
                return _paymentRepository.GetTotalAmountByMonth(year, month);
            }
            catch (Exception ex)
            {
                throw new PaymentProcessingException("Failed to calculate total amount.");
            }
        }

        public PaymentResponse ProcessPayment(int orderId, string method, string? cardNumber, string? upiId)
        {
            try
            {
                switch (method.ToLower())
                {
                    case "card":
                        if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length != 16)
                            throw new PaymentProcessingException("Card number must be exactly 16 digits.");
                        return _paymentRepository.ProcessCardPayment(orderId, cardNumber);

                    case "upi":
                        if (string.IsNullOrWhiteSpace(upiId) || upiId.Length > 50 || !Regex.IsMatch(upiId, @"^\w+@\w+$"))
                            throw new PaymentProcessingException("Invalid UPI ID format or length.");
                        return _paymentRepository.ProcessUPIPayment(orderId, upiId);

                    case "cod":
                        return _paymentRepository.ProcessCODPayment(orderId);

                    default:
                        throw new PaymentProcessingException("Invalid payment method. Use 'card', 'upi', or 'cod'.");
                }
            }
            catch (OrderNotExistsException ex)
            {
                throw new OrderNotExistsException(orderId); 
            }
            catch (PaymentProcessingException ex)
            {
                throw new PaymentProcessingException(ex.Message); 
            }
            catch (Exception)
            {
                throw new PaymentProcessingException("An unexpected error occurred during payment processing.");
            }
        }
        public PaymentResponse UpdatePaymentMethod(int paymentId, string newMethod, string? cardNumber, string? upiId)
        {
            try
            {
                var payment = _paymentRepository.GetPaymentById(paymentId);
                if (payment == null)
                    throw new OrderNotExistsException(paymentId);

                if (payment.Method != PaymentMethod.CashOnDelivery)
                    throw new PaymentProcessingException("Only COD payments can be updated.");

                return _paymentRepository.UpdatePaymentMethod(paymentId, newMethod, cardNumber, upiId);
            }
            catch (OrderNotExistsException)
            {
                throw;
            }
            catch (PaymentProcessingException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new PaymentProcessingException("Failed to update payment method.");
            }
        }

        public string DeletePayment(int paymentId)
        {
            try
            {
                var payment = _paymentRepository.GetPaymentById(paymentId);
                if (payment == null)
                    throw new OrderNotExistsException(paymentId);

                _paymentRepository.DeletePayment(payment);
                return "Payment deleted successfully.";
            }
            catch (Exception)
            {
                throw new PaymentProcessingException("Failed to delete payment.");
            }
        }
    }
}