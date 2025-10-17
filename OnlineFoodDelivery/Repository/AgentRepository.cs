using Microsoft.EntityFrameworkCore;
using OnlineFoodDelivery.Data;
using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Repository
{
    public class AgentRepository : IAgentRepository
    {
        private readonly OnlineFoodDeliveryContext _context;
        public AgentRepository(OnlineFoodDeliveryContext context)
        {
            _context = context;
        }

        public List<DeliveryAgent> GetAllAgents()
        {
            return _context.DeliveryAgent
                .Include(a => a.User) 
                .ToList();
        }

        //public List<DeliveryAgent> GetAllAgents()
        //{
        //    return _context.DeliveryAgent.ToList();
        //}

        public int AddAgent(DeliveryAgent agent)
        {
            _context.DeliveryAgent.Add(agent);
            return _context.SaveChanges();
        }

        public DeliveryAgent GetAgentById(int id)
        {
            return _context.DeliveryAgent.FirstOrDefault(a => a.AgentId == id);
        }

        public int DeleteAgent(int id)
        {
            var agent = _context.DeliveryAgent.FirstOrDefault(a => a.AgentId == id);
            _context.DeliveryAgent.Remove(agent);
            return _context.SaveChanges();

            //if (agent == null)
            //{
            //    throw new AgentNotFoundException($"Agent with agent id {id} does not exist");
            //}

        }


        public int UpdateAgent(int id, DeliveryAgent newAgent)
        {
            var agent = _context.DeliveryAgent.FirstOrDefault(a => a.AgentId == id);
            agent.AgentStatus = newAgent.AgentStatus;
            agent.VehicleNumber = newAgent.VehicleNumber;

            _context.DeliveryAgent.Update(agent);
            return _context.SaveChanges();

            //if (agent == null)
            //{
            //    throw new AgentNotFoundException($"Agent with agent id {id} does not exist");
            //}

            //agent.AgentName = newAgent.AgentName;
            //agent.AgentPhno = newAgent.AgentPhno;
            //agent.AgentEmail = newAgent.AgentEmail;
        }

    }
}

