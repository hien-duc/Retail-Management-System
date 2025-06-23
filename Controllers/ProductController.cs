using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Retail_Management_System.Data;
using Retail_Management_System.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Management_System.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index(string searchString, int? categoryId, int page = 1)
        {
            // Get all categories for the filter dropdown
            ViewData["CategoryList"] = new SelectList(_context.Categories, "Id", "CategoryName");
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentCategory"] = categoryId;
            
            // Start with all products
            var productsQuery = _context.Products
                .Include(p => p.Category)
                .AsQueryable();
            
            // Apply search filter if provided
            if (!string.IsNullOrEmpty(searchString))
            {
                productsQuery = productsQuery.Where(p => p.ProductName.Contains(searchString));
            }
            
            // Apply category filter if provided
            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryID == categoryId.Value);
            }
            
            // Pagination settings
            int pageSize = 10;
            int totalItems = await productsQuery.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            
            // Ensure page is within valid range
            page = Math.Max(1, Math.Min(page, Math.Max(1, totalPages)));
            
            // Get the products for the current page
            var products = await productsQuery
                .OrderBy(p => p.ProductName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            // Set pagination data for the view
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;
            
            return View(products);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        [Authorize(Roles = "Admin,Manager")] // Only Admin and Manager can access
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")] // Only Admin and Manager can access
        public async Task<IActionResult> Create([Bind("ProductCode,ProductName,Description,SKU,CategoryID,Price,CostPrice,StockQuantity,ProductImage,IsActive")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreatedDate = DateTime.Now;
                product.Status = 1; // Active status
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "CategoryName", product.CategoryID);
            return View(product);
        }

        // GET: Product/Edit/5
        [Authorize(Roles = "Admin,Manager")] // Only Admin and Manager can access
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "CategoryName", product.CategoryID);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")] // Only Admin and Manager can access
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductCode,ProductName,Description,SKU,CategoryID,Price,CostPrice,StockQuantity,ProductImage,IsActive,Status,CreatedDate")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "CategoryName", product.CategoryID);
            return View(product);
        }

        // GET: Product/Delete/5
        [Authorize(Roles = "Admin,Manager")] // Only Admin and Manager can access
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")] // Only Admin and Manager can access
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}