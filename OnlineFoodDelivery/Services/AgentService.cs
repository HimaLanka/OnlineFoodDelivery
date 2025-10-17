using OnlineFoodDelivery.Exceptions;
using OnlineFoodDelivery.Model;
using OnlineFoodDelivery.Repository;

namespace OnlineFoodDelivery.Services
{
    public class AgentService : IAgentService
    {
        private readonly IAgentRepository repository;

        public AgentService(IAgentRepository agentRepository)
        {
            repository = agentRepository;
        }

        public List<DeliveryAgent> GetAllAgents()
        {
            return repository.GetAllAgents();
        }

        public DeliveryAgent GetAgentById(int id)
        {
            var agent = repository.GetAgentById(id);
            if (agent == null)
            {
                throw new AgentIDNotFoundException($"Agent with agent id: {id} does not exist");
            }
            return agent;
        }


        public int AddAgent(DeliveryAgent agent)
        {
            if (repository.GetAgentById(agent.AgentId) != null)
            {
                throw new AgentAlreadyExistsException($"Agent with agent id {agent.AgentId} already exists");
            }
            return repository.AddAgent(agent);
        }


        public int DeleteAgent(int id)
        {
            if (repository.GetAgentById(id) == null)
            {
                throw new AgentIDNotFoundException($"Agent with agent id {id} does not exist");
            }
            return repository.DeleteAgent(id);
        }


        public int UpdateAgent(int id, DeliveryAgent agent)
        {
            if (repository.GetAgentById(id) == null)
            {
                throw new AgentIDNotFoundException($"Agent with agent id {id} does not exist");
            }

            return repository.UpdateAgent(id, agent);
        } 
    }
}
