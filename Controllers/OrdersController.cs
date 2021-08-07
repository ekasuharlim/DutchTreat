using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
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
        private readonly IMapper mapper;

        public OrdersController(IDutchRepository repo, ILogger<IDutchRepository> logger, IMapper mapper)
        {
            this.repo = repo;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderViewModel>> Get(bool includeOrderItem = false)
        {
            try
            {
                var orders = repo.GetAllOrder(includeOrderItem);
                return Ok(mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(orders));
            }
            catch (Exception ex)
            {
                logger.LogError($"Error {ex.Message}");
                return BadRequest("Error get order");
            }
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult<OrderViewModel> Get(int id) {
            try
            {
                var order = repo.GetOrder(id);
                if (order == null)
                {
                    return NotFound("Order id not found");
                }
                 else { 
                    return Ok(mapper.Map<Order,OrderViewModel>(order)); 
                }
            }
            catch (Exception ex) {
                logger.LogError($"Error {ex.Message}");
                return BadRequest("Error get order");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody]OrderViewModel orderVm) {
            try
            {
                if (ModelState.IsValid)
                {
                    //save
                    var order = new Order
                    {
                        OrderNumber = orderVm.OrderNumber,
                        OrderDate = orderVm.OrderDate,
                        Id = orderVm.OrderId
                    };
                    repo.AddEntity(order);
                    if (repo.SaveAll()) {
                        return Created($"api/orders/{order.Id}", order);
                    }
                    else {
                        return BadRequest("Save error");
                    }
                    
                }
                else 
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error saving {ex.Message}");
                return BadRequest("Error saving");
            }

        }
    }
}
