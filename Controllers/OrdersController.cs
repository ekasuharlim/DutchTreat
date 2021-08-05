using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("api/{Controller}")]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository repo;
        private readonly ILogger<IDutchRepository> logger;

        public OrdersController(IDutchRepository repo, ILogger<IDutchRepository> logger)
        {
            this.repo = repo;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            try
            {
                var orders = repo.GetAllOrder();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error {ex.Message}");
                return BadRequest("Error get order");
            }
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult<Order> Get(int id) {
            try
            {
                var order = repo.GetOrder(id);
                if (order == null)
                {
                    return NotFound("Order id not found");
                }
                 else { 
                    return Ok(order); 
                }
            }
            catch (Exception ex) {
                logger.LogError($"Error {ex.Message}");
                return BadRequest("Error get order");
            }
        }
    }
}
