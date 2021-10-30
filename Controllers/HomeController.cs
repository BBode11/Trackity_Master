using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Trackity.Models;
using Microsoft.EntityFrameworkCore;

namespace Trackity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ExpenseContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, ExpenseContext ctx)
        {
            _logger = logger;
            context = ctx;
        }

        public IActionResult Index()
        {
            var expenses = context.Expenses.Include(e => e.ExpenseType)
                .OrderBy(e => e.Name).ToList();
            return View(expenses);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
