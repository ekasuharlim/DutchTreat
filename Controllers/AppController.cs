using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DutchTreat.ViewModels;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
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
                //do some actions
            }
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

    }
}
