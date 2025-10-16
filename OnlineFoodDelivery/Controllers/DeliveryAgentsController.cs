using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFoodDelivery.Exceptions;
using OnlineFoodDelivery.Model;
using OnlineFoodDelivery.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryAgentsController : ControllerBase
    {
        private readonly IAgentService _service;

        public DeliveryAgentsController(IAgentService service)
        {
            _service = service;
        }


        [HttpGet]
        public IActionResult GetAgent()
        {
            return Ok(_service.GetAllAgents());
        }


        [HttpPost]
        public ActionResult PostAgent(DeliveryAgent agent)
        {
            try
            {
                var result = _service.AddAgent(agent);
                return StatusCode(201, result);
            }
            catch (AgentAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAgent(int id)
        {
            try
            {
                var result = _service.DeleteAgent(id);
                return Ok(result);
            }
            catch (AgentIDNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutAgent(int id, DeliveryAgent agent)
        {
            try
            {
                var result = _service.UpdateAgent(id, agent);
                return Ok(result);
            }
            catch (AgentIDNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetAgentById(int id)
        {
            try
            {
                var agent = _service.GetAgentById(id);
                return Ok(agent);
            }
            catch (AgentIDNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /*
        [HttpPost]
        public ActionResult PostAgent(DeliveryAgent agent)
        {
            return StatusCode(201, _service.AddAgent(agent));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAgent(int id)
        {
            return Ok(_service.DeleteAgent(id));
        }

        [HttpPut("{id}")]
        public IActionResult PutAgent(int id, DeliveryAgent agent)
        {
            return Ok(_service.UpdateAgent(id, agent));
        }
    }


      // GET: api/DeliveryAgents
      [HttpGet]
      public async Task<ActionResult<IEnumerable<DeliveryAgent>>> GetDeliveryAgent()
      {
          return await _context.DeliveryAgent.ToListAsync();
      }

      // GET: api/DeliveryAgents/5
      [HttpGet("{id}")]
      public async Task<ActionResult<DeliveryAgent>> GetDeliveryAgent(int id)
      {
          var deliveryAgent = await _context.DeliveryAgent.FindAsync(id);

          if (deliveryAgent == null)
          {
              return NotFound();
          }

          return deliveryAgent;
      }

      // PUT: api/DeliveryAgents/5
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPut("{id}")]
      public async Task<IActionResult> PutDeliveryAgent(int id, DeliveryAgent deliveryAgent)
      {
          if (id != deliveryAgent.AgentId)
          {
              return BadRequest();
          }

          _context.Entry(deliveryAgent).State = EntityState.Modified;

          try
          {
              await _context.SaveChangesAsync();
          }
          catch (DbUpdateConcurrencyException)
          {
              if (!DeliveryAgentExists(id))
              {
                  return NotFound();
              }
              else
              {
                  throw;
              }
          }

          return NoContent();
      }

      // POST: api/DeliveryAgents
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPost]
      public async Task<ActionResult<DeliveryAgent>> PostDeliveryAgent(DeliveryAgent deliveryAgent)
      {
          _context.DeliveryAgent.Add(deliveryAgent);
          await _context.SaveChangesAsync();

          return CreatedAtAction("GetDeliveryAgent", new { id = deliveryAgent.AgentId }, deliveryAgent);
      }

      // DELETE: api/DeliveryAgents/5
      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteDeliveryAgent(int id)
      {
          var deliveryAgent = await _context.DeliveryAgent.FindAsync(id);
          if (deliveryAgent == null)
          {
              return NotFound();
          }

          _context.DeliveryAgent.Remove(deliveryAgent);
          await _context.SaveChangesAsync();

          return NoContent();
      }

      private bool DeliveryAgentExists(int id)
      {
          return _context.DeliveryAgent.Any(e => e.AgentId == id);
      } */


    }
}

