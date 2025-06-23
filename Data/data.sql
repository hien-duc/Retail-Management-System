-- Insert Roles
INSERT INTO Roles (RoleName, Status, CreatedDate) VALUES
('Admin', 1, '2025-06-20 12:27:08'),
('Manager', 1, '2025-06-20 12:27:08'),
('Staff', 1, '2025-06-20 12:27:08');

-- Insert Users
INSERT INTO Users (Username, Password, Email, FullName, RoleID, Status, CreatedDate) VALUES
('admin', 'admin123', 'admin@retailstore.com', 'Admin User', 1, 1, '2025-06-20 12:27:08'),
('manager', 'manager123', 'manager@retailstore.com', 'Manager User', 2, 1, '2025-06-20 12:27:08'),
('staff1', 'staff123', 'staff1@retailstore.com', 'Staff User 1', 3, 1, '2025-06-20 12:27:08'),
('staff2', 'staff123', 'staff2@retailstore.com', 'Staff User 2', 3, 1, '2025-06-20 12:27:08');

-- Insert Categories
INSERT INTO Categories (CategoryCode, CategoryName, Description, Status, CreatedDate) VALUES
('ELEC', 'Electronics', 'Electronic devices and accessories', 1, '2025-06-20 12:27:08'),
('CLOTH', 'Clothing', 'Apparel and fashion items', 1, '2025-06-20 12:27:08'),
('BOOKS', 'Books', 'Books and publications', 1, '2025-06-20 12:27:08'),
('HOME', 'Home And Garden', 'Home decor and garden supplies', 1, '2025-06-20 12:27:08');

-- Insert Products
INSERT INTO Products (ProductCode, ProductName, Description, SKU, CategoryID, Price, CostPrice, StockQuantity, ProductImage, IsActive, Status, CreatedDate) VALUES
('ELEC001', 'Smartphone X', 'Latest smartphone with advanced features', 'SM-X-001', 1, 799.99, 599.99, 50, '/images/products/smartphone.jpg', 1, 1, '2025-06-20 12:27:08'),
('ELEC002', 'Laptop Pro', 'High-performance laptop for professionals', 'LP-PRO-002', 1, 1299.99, 999.99, 30, '/images/products/laptop.jpg', 1, 1, '2025-06-20 12:27:08'),
('CLOTH001', 'Designer Jeans', 'Premium quality designer jeans', 'DJ-001', 2, 89.99, 49.99, 100, '/images/products/jeans.jpg', 1, 1, '2025-06-20 12:27:08'),
('BOOK001', 'Programming Guide', 'Comprehensive programming guide for beginners', 'PG-001', 3, 39.99, 19.99, 75, '/images/products/book.jpg', 1, 1, '2025-06-20 12:27:08'),
('HOME001', 'Decorative Lamp', 'Modern decorative lamp for home', 'DL-001', 4, 59.99, 29.99, 40, '/images/products/lamp.jpg', 1, 1, '2025-06-20 12:27:08'),
('HOME002', 'Garden Tools Set', 'Complete set of essential garden tools', 'GTS-001', 4, 79.99, 39.99, 25, '/images/products/gardentools.jpg', 1, 1, '2025-06-20 12:27:08');

-- Insert Promotions
INSERT INTO Promotions (Title, Description, ImagePath, IsActive, Status, CreatedDate) VALUES
('Summer Sale', 'Get up to 50% off on all summer products', '/promotion/summer-sale.jpg', 1, 1, '2025-06-20 12:27:08'),
('New Electronics', 'Check out our latest electronic gadgets', '/promotion/new-electronics.jpg', 1, 1, '2025-06-20 12:27:08'),
('Holiday Special', 'Special discounts for the holiday season', '/promotion/holiday-special.jpg', 1, 1, '2025-06-20 12:27:08');
