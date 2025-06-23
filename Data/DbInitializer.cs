using Microsoft.EntityFrameworkCore;
using Retail_Management_System.Models;
using System;
using System.Linq;

namespace Retail_Management_System.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Create database if it doesn't exist
            context.Database.EnsureCreated();

            // Check if there's already data in the database
            if (context.Products.Any())
            {
                return; // Database has been seeded
            }

            // Seed Roles
            var roles = new Role[]
            {
                new Role { RoleName = "Admin", Status = 1, CreatedDate = DateTime.Now },
                new Role { RoleName = "Manager", Status = 1, CreatedDate = DateTime.Now },
                new Role { RoleName = "Staff", Status = 1, CreatedDate = DateTime.Now }
            };

            context.Roles.AddRange(roles);
            context.SaveChanges();

            // Seed Users
            var users = new User[]
            {
                new User
                {
                    Username = "admin",
                    Password = "admin123", // In a real app, this should be hashed
                    Email = "admin@retailstore.com",
                    FullName = "Admin User",
                    RoleID = roles[0].Id, // Admin
                    Status = 1,
                    CreatedDate = DateTime.Now
                },
                new User
                {
                    Username = "manager",
                    Password = "manager123", // In a real app, this should be hashed
                    Email = "manager@retailstore.com",
                    FullName = "Manager User",
                    RoleID = roles[1].Id, // Manager
                    Status = 1,
                    CreatedDate = DateTime.Now
                },
                new User
                {
                    Username = "staff1",
                    Password = "staff123", // In a real app, this should be hashed
                    Email = "staff1@retailstore.com",
                    FullName = "Staff User 1",
                    RoleID = roles[2].Id, // Staff
                    Status = 1,
                    CreatedDate = DateTime.Now
                },
                new User
                {
                    Username = "staff2",
                    Password = "staff123", // In a real app, this should be hashed
                    Email = "staff2@retailstore.com",
                    FullName = "Staff User 2",
                    RoleID = roles[2].Id, // Staff
                    Status = 1,
                    CreatedDate = DateTime.Now
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            // Seed Categories
            var categories = new Category[]
            {
                new Category
                {
                    CategoryCode = "ELEC",
                    CategoryName = "Electronics",
                    Description = "Electronic devices and accessories",
                    Status = 1,
                    CreatedDate = DateTime.Now
                },
                new Category
                {
                    CategoryCode = "CLOTH",
                    CategoryName = "Clothing",
                    Description = "Apparel and fashion items",
                    Status = 1,
                    CreatedDate = DateTime.Now
                },
                new Category
                {
                    CategoryCode = "BOOKS",
                    CategoryName = "Books",
                    Description = "Books and publications",
                    Status = 1,
                    CreatedDate = DateTime.Now
                },
                new Category
                {
                    CategoryCode = "HOME",
                    CategoryName = "Home And Garden",
                    Description = "Home decor and garden supplies",
                    Status = 1,
                    CreatedDate = DateTime.Now
                }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            // Seed Products
            var products = new Product[]
            {
                new Product
                {
                    ProductCode = "ELEC001",
                    ProductName = "Smartphone X",
                    Description = "Latest smartphone with advanced features",
                    SKU = "SM-X-001",
                    CategoryID = categories[0].Id, // Electronics
                    Price = 799.99M,
                    CostPrice = 599.99M,
                    StockQuantity = 50,
                    ProductImage = "/images/products/smartphone.jpg",
                    IsActive = true,
                    Status = 1,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    ProductCode = "ELEC002",
                    ProductName = "Laptop Pro",
                    Description = "High-performance laptop for professionals",
                    SKU = "LP-PRO-002",
                    CategoryID = categories[0].Id, // Electronics
                    Price = 1299.99M,
                    CostPrice = 999.99M,
                    StockQuantity = 30,
                    ProductImage = "/images/products/laptop.jpg",
                    IsActive = true,
                    Status = 1,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    ProductCode = "CLOTH001",
                    ProductName = "Designer Jeans",
                    Description = "Premium quality designer jeans",
                    SKU = "DJ-001",
                    CategoryID = categories[1].Id, // Clothing
                    Price = 89.99M,
                    CostPrice = 49.99M,
                    StockQuantity = 100,
                    ProductImage = "/images/products/jeans.jpg",
                    IsActive = true,
                    Status = 1,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    ProductCode = "BOOK001",
                    ProductName = "Programming Guide",
                    Description = "Comprehensive programming guide for beginners",
                    SKU = "PG-001",
                    CategoryID = categories[2].Id, // Books
                    Price = 39.99M,
                    CostPrice = 19.99M,
                    StockQuantity = 75,
                    ProductImage = "/images/products/book.jpg",
                    IsActive = true,
                    Status = 1,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    ProductCode = "HOME001",
                    ProductName = "Decorative Lamp",
                    Description = "Modern decorative lamp for home",
                    SKU = "DL-001",
                    CategoryID = categories[3].Id, // Home And Garden
                    Price = 59.99M,
                    CostPrice = 29.99M,
                    StockQuantity = 40,
                    ProductImage = "/images/products/lamp.jpg",
                    IsActive = true,
                    Status = 1,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    ProductCode = "HOME002",
                    ProductName = "Garden Tools Set",
                    Description = "Complete set of essential garden tools",
                    SKU = "GTS-001",
                    CategoryID = categories[3].Id, // Home And Garden
                    Price = 79.99M,
                    CostPrice = 39.99M,
                    StockQuantity = 25,
                    ProductImage = "/images/products/gardentools.jpg",
                    IsActive = true,
                    Status = 1,
                    CreatedDate = DateTime.Now
                }
            };

            context.Products.AddRange(products);
            context.SaveChanges();

            // Seed Promotions
            var promotions = new Promotion[]
            {
                new Promotion
                {
                    Title = "Summer Sale",
                    Description = "Get up to 50% off on all summer products",
                    ImagePath = "/promotion/summer-sale.jpg",
                    IsActive = true,
                    Status = 1,
                    CreatedDate = DateTime.Now
                },
                new Promotion
                {
                    Title = "New Electronics",
                    Description = "Check out our latest electronic gadgets",
                    ImagePath = "/promotion/new-electronics.jpg",
                    IsActive = true,
                    Status = 1,
                    CreatedDate = DateTime.Now
                },
                new Promotion
                {
                    Title = "Holiday Special",
                    Description = "Special discounts for the holiday season",
                    ImagePath = "/promotion/holiday-special.jpg",
                    IsActive = true,
                    Status = 1,
                    CreatedDate = DateTime.Now
                }
            };

            context.Promotions.AddRange(promotions);
            context.SaveChanges();
        }
    }
}