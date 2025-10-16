using OnlineFoodDelivery.Data;
using OnlineFoodDelivery.Exceptions;
using OnlineFoodDelivery.Migrations;
using OnlineFoodDelivery.Model;
using System.Text.RegularExpressions;

namespace OnlineFoodDelivery.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly OnlineFoodDeliveryContext _context;

        public PaymentRepository(OnlineFoodDeliveryContext context)
        {
            _context = context;
        }

        public List<Payment> GetAllPayments() => _context.Payment.ToList();

        public decimal GetTotalAmountByMonth(int year, int month)
        {
            return _context.Payment
                .Where(p => p.PaymentDate.Year == year && p.PaymentDate.Month == month && p.IsSuccess)
                .Sum(p => p.Amount);
        }

        public Payment GetPaymentById(int paymentId)
        {
            return _context.Payment.FirstOrDefault(p => p.PaymentId == paymentId);
        }

        public PaymentResponse ProcessCardPayment(int orderId, string cardNumber)
        {
            var order = _context.Order.Find(orderId);
            if (order == null) throw new OrderNotExistsException(orderId);

            if (_context.Payment.Any(p => p.OrderId == orderId))
                return new PaymentResponse { Status = "Failed", Message = "Payment already exists." };

            var payment = new Payment
            {
                OrderId = orderId,
                Amount = order.TotalAmount,
                Method = PaymentMethod.Card,
                CardNumber = cardNumber,
                CardType = "Visa",
                IsSuccess = true
            };

            _context.Payment.Add(payment);
            _context.SaveChanges();

            return new PaymentResponse { Status = "Success", Message = "Card payment successful", TotalAmount = payment.Amount };
        }

        public PaymentResponse ProcessUPIPayment(int orderId, string upiId)
        {
            var order = _context.Order.Find(orderId);
            if (order == null) throw new OrderNotExistsException(orderId);

            if (_context.Payment.Any(p => p.OrderId == orderId))
                return new PaymentResponse { Status = "Failed", Message = "Payment already exists." };

            var payment = new Payment
            {
                OrderId = orderId,
                Amount = order.TotalAmount,
                Method = PaymentMethod.UPI,
                UpiId = upiId,
                UpiProvider = upiId.Split('@')[1],
                IsSuccess = true
            };

            _context.Payment.Add(payment);
            _context.SaveChanges();

            return new PaymentResponse { Status = "Success", Message = "UPI payment successful", TotalAmount = payment.Amount };
        }

        public PaymentResponse ProcessCODPayment(int orderId)
        {
            var order = _context.Order.Find(orderId);
            if (order == null) throw new OrderNotExistsException(orderId);

            if (_context.Payment.Any(p => p.OrderId == orderId))
                return new PaymentResponse { Status = "Failed", Message = "Payment already exists." };

            var payment = new Payment
            {
                OrderId = orderId,
                Amount = order.TotalAmount,
                Method = PaymentMethod.CashOnDelivery,
                IsCashOnDelivery = true,
                IsSuccess = true
            };

            _context.Payment.Add(payment);
            _context.SaveChanges();

            return new PaymentResponse { Status = "Success", Message = "COD payment successful", TotalAmount = payment.Amount };
        }

        public PaymentResponse UpdatePaymentMethod(int paymentId, string newMethod, string? cardNumber, string? upiId)
        {
            var payment = _context.Payment.FirstOrDefault(p => p.PaymentId == paymentId);
            if (payment == null)
                throw new OrderNotExistsException(paymentId);

            switch (newMethod.ToLower())
            {
                case "card":
                    if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length != 16)
                        throw new PaymentProcessingException("Card number must be exactly 16 digits.");

                    payment.Method = PaymentMethod.Card;
                    payment.CardNumber = cardNumber;
                    payment.CardType = "Visa";
                    payment.IsCashOnDelivery = false;
                    break;

                case "upi":
                    if (string.IsNullOrWhiteSpace(upiId) || upiId.Length > 50 || !Regex.IsMatch(upiId, @"^\w+@\w+$"))
                        throw new PaymentProcessingException("Invalid UPI ID format or length.");

                    payment.Method = PaymentMethod.UPI;
                    payment.UpiId = upiId;
                    payment.UpiProvider = upiId.Split('@')[1];
                    payment.IsCashOnDelivery = false;
                    break;

                default:
                    throw new PaymentProcessingException("Invalid payment method. Use 'card' or 'upi'.");
            }

            _context.SaveChanges();

            return new PaymentResponse
            {
                Status = "Success",
                Message = $"Payment method updated to {newMethod}.",
                TotalAmount = payment.Amount
            };
        }

        public void DeletePayment(Payment payment)
        {
            _context.Payment.Remove(payment);
            _context.SaveChanges();
        }

    }
}
