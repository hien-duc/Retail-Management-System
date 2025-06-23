using Microsoft.AspNetCore.Mvc;
using Retail_Management_System.Data;
using Retail_Management_System.Models;
using System.Diagnostics;

namespace Retail_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Get promotions for carousel
            var promotions = _context.Promotions.Where(p => p.IsActive).ToList();
            
            // Get statistics for display on homepage
            ViewBag.ProductCount = _context.Products.Count();
            ViewBag.CategoryCount = _context.Categories.Count();
            ViewBag.LowStockCount = _context.Products.Count(p => p.StockQuantity < 10);
            
            return View(promotions);
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
