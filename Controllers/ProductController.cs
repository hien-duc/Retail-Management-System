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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> Create([Bind("ProductCode,ProductName,Description,SKU,CategoryID,Price,CostPrice,StockQuantity,ProductImage,IsActive")] Product product, IFormFile? productImg)
        {
            if (ModelState.IsValid)
            {
                // Handle cover image upload
                if (productImg != null && productImg.Length > 0)
                {
                    product.ProductImage = await UploadFile(productImg, "images/products");
                }
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductCode,ProductName,Description,SKU,CategoryID,Price,CostPrice,StockQuantity,ProductImage,IsActive,Status,CreatedDate")] Product product, IFormFile? productImg)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                // Handle cover image upload if a new one is provided
                if (productImg != null && productImg.Length > 0)
                {
                    // Delete old cover image if it exists
                    if (!string.IsNullOrEmpty(product.ProductImage))
                    {
                        DeleteFile(product.ProductImage);
                    }

                    product.ProductImage = await UploadFile(productImg, "images/products");
                }
                
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
        
        private async Task<string> UploadFile(IFormFile file, string folderPath)
        {
            if (file == null || file.Length == 0)
                return null;

            // Create the directory if it doesn't exist
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Generate a unique file name
            string uniqueFileName = $"{Guid.NewGuid().ToString()}_{Path.GetFileName(file.FileName)}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Save the file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Return the relative path
            return $"/{folderPath.Replace("\\", "/")}/{uniqueFileName}";
        }

        private void DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            // Convert the URL path to a physical file path
            var relativePath = filePath.StartsWith('/') ? filePath.Substring(1) : filePath;
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, relativePath);

            // Delete the file if it exists
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
    }
}