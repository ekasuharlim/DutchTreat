using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DutchTreat.ViewModels;
using DutchTreat.Services;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService mailService;

        public AppController(IMailService mailService)
        {
            this.mailService = mailService;
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

    }
}
