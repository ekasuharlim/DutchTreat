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
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductController : Controller 
    {
        private readonly IDutchRepository repository;
        private readonly ILogger<ProductController> logger;

        public ProductController(IDutchRepository repository, ILogger<ProductController> logger) {
            this.repository = repository;
            this.logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Product>> Get() {
            try
            {
                this.logger.LogInformation("Start fetching product");
                var result = this.repository.GetAllProducts();
                this.logger.LogInformation("Finish fetching product");
                return Ok(result);
            }
            catch (Exception ex) {
                logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
            
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<IEnumerable<Product>> Get(int id)
        {
            try
            {
                return Ok(this.repository.GetProduct(id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }


    }
}
