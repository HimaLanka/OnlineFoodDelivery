
using Microsoft.EntityFrameworkCore;
using OnlineFoodDelivery.Data;
using OnlineFoodDelivery.EmailNotificationsService;
using OnlineFoodDelivery.Model;
using System.Globalization;

namespace OnlineFoodDelivery.Repository
{
    public class DeliveryStatusRepository : IDeliveryStatusRepository
    {
        private readonly OnlineFoodDeliveryContext _context;
        private readonly IEmailService _emailService;

        public DeliveryStatusRepository(OnlineFoodDeliveryContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }


        public DeliveryStatus AssignAgentToOrder(int orderId)
        {
            var order = _context.Order
                .Include(o => o.Payments)
                .FirstOrDefault(o => o.OrderId == orderId);

            // Check if order exists and has at least one successful payment
            if (order == null || !order.Payments.Any(p => p.IsSuccess))
            {
                return null; // Order not found or payment not completed
            }

            var agent = _context.DeliveryAgent.FirstOrDefault(a => a.AgentStatus == "Available");
            if (agent == null)
            {
                return new DeliveryStatus
                {
                    OrderId = -1 // No agent available
                };
            }

            var delivery = new DeliveryStatus
            {
                OrderId = orderId,
                AgentId = agent.AgentId,
                StatusOfDelivey = "In Progress",
                EstimatedTimeOfArrival = DateTime.Now.AddMinutes(30)
            };

            agent.AgentStatus = "Busy";
            _context.DeliveryStatus.Add(delivery);
            _context.SaveChanges();
            return delivery;
        }



        //public DeliveryStatus AssignAgentToOrder(int orderId)
        //{
        //    var agent = _context.DeliveryAgent.FirstOrDefault(a => a.AgentStatus == "Available");
        //    if (agent == null)
        //        //throw new AgentsNotAvailableException("No available agents");
        //        return null;

        //    var delivery = new DeliveryStatus
        //    {
        //        OrderId = orderId,
        //        AgentId = agent.AgentId,
        //        StatusOfDelivey = "In Progress",
        //        EstimatedTimeOfArrival = DateTime.Now.AddMinutes(30)
        //    };

        //    agent.AgentStatus = "Busy";
        //    _context.DeliveryStatus.Add(delivery);
        //    _context.SaveChanges();
        //    return delivery;
        //}


        public DeliveryStatus GetDeliveryById(int deliveryId)
        {
            return _context.DeliveryStatus.Include(d => d.Order).Include(d => d.Agent).FirstOrDefault(d => d.DeliveryId == deliveryId);
            //return _context.DeliveryStatus.FirstOrDefault(d => d.DeliveryId == deliveryId);
        }




        //public DeliveryStatus UpdateDeliveryStatus(int deliveryId, string newStatus)
        //public async Task<DeliveryStatus> UpdateDeliveryStatus(int deliveryId, string newStatus)
        //{
        //    var delivery = await _context.DeliveryStatus
        //        .Include(d => d.Order)
        //        .Include(d => d.Agent)
        //        .FirstOrDefaultAsync(d => d.DeliveryId == deliveryId);

        //    if (delivery == null) throw new DeliveryByIDNotFoundException($"Delivery with delivery id {deliveryId} does not exist");

        //    delivery.StatusOfDelivey = newStatus;

        //    if (newStatus == "Delivered" && delivery.Agent != null)
        //    {
        //        delivery.Agent.AgentStatus = "Available";
        //        _context.DeliveryAgent.Update(delivery.Agent);

        //        var customer = await _context.Users
        //            .FirstOrDefaultAsync(u => u.UserId == delivery.Order.UserId);


        //        if (customer == null)
        //        {
        //            throw new CustomerNotFoundException("Customer cannot be null");
        //        }

        //        await _emailService.SendEmailAsync(
        //            customer.Email,
        //            "Your Order Has Been Delivered",
        //            $"Hi {customer.Username}, your order #{delivery.OrderId} has been successfully delivered!"
        //        );

        //    }

        //    await _context.SaveChangesAsync();
        //    return delivery;
        //}


        public async Task<DeliveryStatus> UpdateDeliveryStatus(int deliveryId, string newStatus)
        {
            var delivery = await _context.DeliveryStatus
                .Include(d => d.Order)
                .Include(d => d.Agent)
                .FirstOrDefaultAsync(d => d.DeliveryId == deliveryId);

            if (delivery == null)
                return null;

            delivery.StatusOfDelivey = newStatus;

            if (newStatus == "Delivered" && delivery.Agent != null)
            {
                delivery.Agent.AgentStatus = "Available";
                _context.DeliveryAgent.Update(delivery.Agent);

                var customer = await _context.User
                    .FirstOrDefaultAsync(u => u.Id == delivery.Order.UserId);

                if (customer != null)
                {
                    await _emailService.SendEmailAsync(
                        customer.Email,
                        "Your Order Has Been Delivered",
                        $"Hi {customer.Username}, your order #{delivery.OrderId} has been successfully delivered!"
                    );
                }
            }

            await _context.SaveChangesAsync();
            return delivery;
        }







        public List<DeliveryStatus> GetAllDeliveries()
        {
            return _context.DeliveryStatus.Include(d => d.Agent).Include(d => d.Order).ToList();
        }



        public Dictionary<string, int> GetMonthlyDeliveryCountByAgent(int agentId)
        {
            return _context.DeliveryStatus
                .Include(d => d.Order) 
                .Where(d => d.AgentId == agentId && d.StatusOfDelivey == "Delivered")
                .GroupBy(d => new { d.Order.OrderDate.Year, d.Order.OrderDate.Month })
                .Select(g => new
                {
                    Month = $"{g.Key.Month}-{g.Key.Year}",
                    Count = g.Count()
                })
                .ToDictionary(x => x.Month, x => x.Count);
        }



        public Dictionary<string, int> GetWeeklyDeliveryCountByAgent(int agentId)
        {

            var calendar = CultureInfo.CurrentCulture.Calendar;

            var deliveries = _context.DeliveryStatus
                .Include(d => d.Order)
                .Where(d => d.AgentId == agentId && d.StatusOfDelivey == "Delivered")
                .ToList()
                .GroupBy(d =>
                {
                    var orderDate = d.Order.OrderDate;
                    int week = calendar.GetWeekOfYear(orderDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

                    // Get first day of the week
                    DateTime firstDayOfWeek = orderDate.AddDays(-(int)orderDate.DayOfWeek + (int)DayOfWeek.Monday);
                    // Get last day of the week
                    DateTime lastDayOfWeek = firstDayOfWeek.AddDays(6);

                    return new
                    {
                        Start = firstDayOfWeek.Date,
                        End = lastDayOfWeek.Date
                    };
                })
                .Select(g => new
                {
                    WeekRange = $"{g.Key.Start:MMM d}–{g.Key.End:MMM d}, {g.Key.Start:yyyy}",
                    Count = g.Count()
                })
                .ToDictionary(x => x.WeekRange, x => x.Count);

            return deliveries;
        }

    }

}















        //return _context.DeliveryStatus
        //               .Include(d => d.Order)
        //               .Include(d => d.Agent)
        //               .ThenInclude(a => a.User)
        //               .FirstOrDefault(d => d.DeliveryId == delivery.DeliveryId);

        //return delivery;



    //public Dictionary<string, int> GetWeeklyDeliveryCountByAgent(int agentId)
    //{
    //    return _context.DeliveryStatus
    //        .Where(d => d.AgentId == agentId && d.StatusOfDelivey == "Delivered")
    //        .GroupBy(d => new
    //        {
    //            Year = d.EstimatedTimeOfArrival.Year,
    //            Week = System.Globalization.CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
    //                d.EstimatedTimeOfArrival,
    //                System.Globalization.CalendarWeekRule.FirstDay,
    //                DayOfWeek.Monday)
    //        })
    //        .Select(g => new
    //        {
    //            Week = $"Week {g.Key.Week}, {g.Key.Year}",
    //            Count = g.Count()
    //        })
    //        .ToDictionary(x => x.Week, x => x.Count);
    //}

    //public DeliveryStatus UpdateDeliveryStatus(int deliveryId, string newStatus)
    //{
    //    var delivery = _context.DeliveryStatus.Include(d => d.Agent).FirstOrDefault(d => d.DeliveryId == deliveryId);
    //    if (delivery == null)
    //        throw new DeliveryNotFoundException("Delivery not found");

    //    delivery.StatusOfDelivey = newStatus;

    //    //if (newStatus == "Delivered")
    //    //    delivery.Agent.AgentStatus = "Available";

    //    if (newStatus == "Delivered" && delivery.Agent != null)
    //    {
    //        delivery.Agent.AgentStatus = "Available";
    //        _context.DeliveryAgent.Update(delivery.Agent); // ✅ Explicit update
    //    }

    //    _context.SaveChanges();
    //    return delivery;
    //}


    //public Dictionary<string, int> GetMonthlyDeliveryCountByAgent(int agentId)
    //{
    //    return _context.DeliveryStatus
    //        .Where(d => d.AgentId == agentId && d.StatusOfDelivey == "Delivered")
    //        .GroupBy(d => new { d.EstimatedTimeOfArrival.Year, d.EstimatedTimeOfArrival.Month })
    //        .Select(g => new
    //        {
    //            Month = $"{g.Key.Month}-{g.Key.Year}",
    //            Count = g.Count()
    //        })
    //        .ToDictionary(x => x.Month, x => x.Count);
    //}


    //public Dictionary<string, int> GetWeeklyDeliveryCountByAgent(int agentId)
    //{
    //    var deliveries = _context.DeliveryStatus
    //        .Where(d => d.AgentId == agentId && d.StatusOfDelivey == "Delivered")
    //        .ToList() 
    //        .GroupBy(d => new
    //        {
    //            Year = d.EstimatedTimeOfArrival.Year,
    //            Week = System.Globalization.CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
    //                d.EstimatedTimeOfArrival,
    //                System.Globalization.CalendarWeekRule.FirstDay,
    //                DayOfWeek.Monday)
    //        })
    //        .Select(g => new
    //        {
    //            Week = $"Week {g.Key.Week}, {g.Key.Year}",
    //            Count = g.Count()
    //        })
    //        .ToDictionary(x => x.Week, x => x.Count);

    //    return deliveries;
    //} 










    //public DeliveryStatus UpdateDeliveryStatus(int deliveryId, string newStatus)
    //{
    //    //    var delivery = _context.DeliveryStatus.Include(d => d.Agent).FirstOrDefault(d => d.DeliveryId == deliveryId);

    //    var delivery = _context.DeliveryStatus
    //        .Include(d => d.Order)
    //        .Include(d => d.Agent)
    //            .ThenInclude(a => a.User)
    //        .FirstOrDefault(d => d.DeliveryId == deliveryId);

    //    if (delivery == null)
    //        throw new DeliveryNotFoundException("Delivery not found");

    //    delivery.StatusOfDelivey = newStatus;

    //    if (newStatus == "Delivered" && delivery.Agent != null)
    //    {
    //        delivery.Agent.AgentStatus = "Available";
    //        _context.DeliveryAgent.Update(delivery.Agent);
    //    }

    //    _context.SaveChanges();
    //    return delivery;
    //}




    //public DeliveryStatus AssignAgentToOrder(int orderId)
    //{
    //    var agent = _context.DeliveryAgent.FirstOrDefault(a => a.AgentStatus == "Available");
    //    if (agent == null)
    //        throw new AgentsNotAvailableException("No available agents");

    //    var delivery = new DeliveryStatus
    //    {
    //        OrderId = orderId,
    //        AgentId = agent.AgentId,
    //        StatusOfDelivey = "In Progress",
    //        EstimatedTimeOfArrival = DateTime.Now.AddMinutes(30)
    //    };

    //    agent.AgentStatus = "Busy";
    //    _context.DeliveryStatus.Add(delivery);
    //    _context.SaveChanges();

    //    var result = _context.DeliveryStatus
    //                         .Include(d => d.Order)
    //                         .Include(d => d.Agent)
    //                             .ThenInclude(a => a.User)
    //                         .FirstOrDefault(d => d.DeliveryId == delivery.DeliveryId);

    //    if (result == null)
    //        throw new DeliveryNotFoundException("Delivery not found after assignment");

    //    return result;


    //}


        //public DeliveryStatus UpdateDeliveryStatus(int deliveryId, string newStatus)
        //{
        //    var delivery = _context.DeliveryStatus
        //        .Include(d => d.Order)
        //            .ThenInclude(o => o.Customer) // ✅ Load customer
        //        .Include(d => d.Agent)
        //            .ThenInclude(a => a.User)     // ✅ Load agent's user
        //        .FirstOrDefault(d => d.DeliveryId == deliveryId);

        //    if (delivery == null)
        //        throw new DeliveryNotFoundException("Delivery not found");

        //    delivery.StatusOfDelivey = newStatus;

        //    if (newStatus == "Delivered" && delivery.Agent != null)
        //    {
        //        delivery.Agent.AgentStatus = "Available";
        //        _context.DeliveryAgent.Update(delivery.Agent);
        //    }

        //    _context.SaveChanges();

        //    return delivery;
        //}



    //if (customer != null)
    //{
    //    try
    //    {
    //        await _emailService.SendEmailAsync(
    //            customer.Email,
    //            "Your Order Has Been Delivered",
    //            $"Hi {customer.Username}, your order #{delivery.OrderId} has been successfully delivered!"
    //        );
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"Email failed: {ex.Message}");
    //    }
    //}





        //public DeliveryStatus AssignAgentToOrder(int orderId)
        //{
        //    var orderExists = _context.Orders.Any(o => o.OrderId == orderId);
        //    if (!orderExists)
        //    {
        //        return null;
        //    }

        //    var agent = _context.DeliveryAgent.FirstOrDefault(a => a.AgentStatus == "Available");
        //    if (agent == null)
        //    {
        //        // Signal that no agent is available
        //        return new DeliveryStatus
        //        {
        //            OrderId = -1 // Use a sentinel value to indicate agent failure
        //        };
        //    }

        //    var delivery = new DeliveryStatus
        //    {
        //        OrderId = orderId,
        //        AgentId = agent.AgentId,
        //        StatusOfDelivey = "In Progress",
        //        EstimatedTimeOfArrival = DateTime.Now.AddMinutes(30)
        //    };

        //    agent.AgentStatus = "Busy";
        //    _context.DeliveryStatus.Add(delivery);
        //    _context.SaveChanges();
        //    return delivery;
        //}
