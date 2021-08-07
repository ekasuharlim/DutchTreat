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

    [Route("/api/Orders/{orderId}/Items")]
    public class OrderItemController : Controller
    {
        private readonly IDutchRepository repo;
        private readonly ILogger<OrderItemController> logger;
        private readonly IMapper mapper;

        public OrderItemController(
            IDutchRepository repo, 
            ILogger<OrderItemController> logger,
            IMapper mapper
            )
        {
            this.repo = repo;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderItemViewModel>> Get(int orderId) {
            try
            {
                var order = repo.GetOrder(orderId);
                return Ok(mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
            }
            catch (Exception ex) {
                logger.LogError(ex.Message);
                return BadRequest("Error Order Item");
            }            
        }
    }
}
