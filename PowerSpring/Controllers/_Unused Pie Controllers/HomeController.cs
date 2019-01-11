using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PowerSpring.Models;
using PowerSpring.ViewModels;

namespace PowerSpring.Controllers
{
    public class HomeController : Controller
    {

        private readonly IPieRepository _pieRepository;

        public HomeController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        public IActionResult Index()
        {
            //var time = DateTime.Now;
            ViewBag.Title = "Power Spring";

            var pies = _pieRepository.Pies.OrderBy(p => p.Name);

            var homeViewModel = new HomeViewModel()
            {
                Pies = pies.ToList(),
                Title = "Welcome to Power Spring - 欢迎来到福源团契"
            };

            return View(homeViewModel);
        }

        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();

            return View(pie);
        }
    }
}