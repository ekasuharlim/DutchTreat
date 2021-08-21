using Microsoft.AspNetCore.Mvc;
using DutchTreat.ViewModels;
using DutchTreat.Services;
using DutchTreat.Data;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService mailService;
        private readonly IDutchRepository repository;
        private readonly ILogger<AppController> logger;

        public AppController(IMailService mailService, IDutchRepository repository,ILogger<AppController> logger)
        {
            this.mailService = mailService;
            this.repository = repository;
            this.logger = logger;
        }

        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel viewmodel)
        {
            if (ModelState.IsValid) 
            {
                this.mailService.SendMessage(viewmodel.Email,viewmodel.Subject, viewmodel.Message);
                ViewBag.EmailActionInfo = "Message Sent";
            }

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Shop() {
            //var products = this.repository.GetAllProducts();
            this.logger.LogInformation("entering shop");
            return View();
        }

    }
}
