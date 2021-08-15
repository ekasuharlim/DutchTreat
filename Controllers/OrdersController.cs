using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository repo;
        private readonly ILogger<IDutchRepository> logger;
        private readonly IMapper mapper;
        private readonly Microsoft.AspNetCore.Identity.UserManager<StoreUser> userManager;

        public OrdersController(IDutchRepository repo, ILogger<IDutchRepository> logger, IMapper mapper, UserManager<StoreUser> userManager)
        {
            this.repo = repo;
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderViewModel>> Get(bool includeOrderItem = false)
        {
            try
            {
                var orders = repo.GetAllOrder(this.User.Identity.Name, includeOrderItem);
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
        public async Task<ActionResult> Post([FromBody]OrderViewModel orderVm) {
            try
            {
                if (ModelState.IsValid)
                {
                    //save
                    var currentUser = await this.userManager.FindByNameAsync(this.User.Identity.Name);
                    var order = new Order
                    {
                        OrderNumber = orderVm.OrderNumber,
                        OrderDate = orderVm.OrderDate,
                        Id = orderVm.OrderId,
                        User = currentUser
                        
                    };
                    repo.AddEntity(order);
                    if (repo.SaveAll()) {                        
                        return Created($"api/orders/{order.Id}", this.mapper.Map<Order, OrderViewModel>(order));
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
