using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Models;
using DutchTreat.Services;
using DutchTreat.Data;
using Microsoft.AspNetCore.Authorization;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IDutchRepo _repo;

        public AppController(IMailService mailService, IDutchRepo repo)
        {
            _mailService = mailService;
            _repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet ("Contact")]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost("Contact")]
        public IActionResult Contact(ContactModels model)
        {
            if(ModelState.IsValid)
            {
                _mailService.SendMessage("mohanad.dabool1@gmail.com", model.Name, $"From: {model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }
            return View();
        }
        [HttpGet("About")]
        public IActionResult About()
        {
            ViewBag.Title = "About US";
            return View();
        }
        [HttpGet("Shop")]
        public IActionResult Shop()
        {
            return View();
        }
    }
}
