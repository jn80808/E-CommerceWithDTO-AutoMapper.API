using Microsoft.EntityFrameworkCore;
using E_Commerce.API.Models;
using E_Commerce.API.Models.Domain;
using ECommerceSystem;

public static class SeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Electronics", Description = "Devices and gadgets" },
            new Category { Id = 2, Name = "Books", Description = "Printed and digital books" },
            new Category { Id = 3, Name = "Clothing", Description = "Apparel and accessories" },
            new Category { Id = 4, Name = "Home Appliances", Description = "Kitchen and home utilities" },
            new Category { Id = 5, Name = "Toys", Description = "Toys and games for kids" }
        );

        // Seed Products
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Smartphone", Description = "Latest model smartphone", Price = 699.99m, StockQuantity = 50 },
            new Product { Id = 2, Name = "Laptop", Description = "High performance laptop", Price = 1299.99m, StockQuantity = 30 },
            new Product { Id = 3, Name = "Fiction Novel", Description = "Bestselling fiction novel", Price = 19.99m, StockQuantity = 100 },
            new Product { Id = 4, Name = "T-Shirt", Description = "100% cotton t-shirt", Price = 15.99m, StockQuantity = 200 },
            new Product { Id = 5, Name = "Blender", Description = "High-speed kitchen blender", Price = 89.99m, StockQuantity = 40 }
        );

        // Seed ProductCategories (Many-to-Many)
        modelBuilder.Entity<ProductCategory>().HasData(
            new ProductCategory { ProductId = 1, CategoryId = 1 }, // Smartphone - Electronics
            new ProductCategory { ProductId = 2, CategoryId = 1 }, // Laptop - Electronics
            new ProductCategory { ProductId = 3, CategoryId = 2 }, // Fiction Novel - Books
            new ProductCategory { ProductId = 4, CategoryId = 3 }, // T-Shirt - Clothing
            new ProductCategory { ProductId = 5, CategoryId = 4 }  // Blender - Home Appliances
        );

        // Seed Orders (10 Orders)
        modelBuilder.Entity<Order>().HasData(
            new Order { Id = 1, CustomerName = "Alice Johnson", OrderDate = DateTime.Now.AddDays(-15) },
            new Order { Id = 2, CustomerName = "Bob Smith", OrderDate = DateTime.Now.AddDays(-13) },
            new Order { Id = 3, CustomerName = "Charlie Davis", OrderDate = DateTime.Now.AddDays(-10) },
            new Order { Id = 4, CustomerName = "David Wilson", OrderDate = DateTime.Now.AddDays(-9) },
            new Order { Id = 5, CustomerName = "Eva Roberts", OrderDate = DateTime.Now.AddDays(-7) },
            new Order { Id = 6, CustomerName = "Fay Green", OrderDate = DateTime.Now.AddDays(-5) },
            new Order { Id = 7, CustomerName = "George Turner", OrderDate = DateTime.Now.AddDays(-3) },
            new Order { Id = 8, CustomerName = "Helen Adams", OrderDate = DateTime.Now.AddDays(-2) },
            new Order { Id = 9, CustomerName = "Ivy Lee", OrderDate = DateTime.Now.AddDays(-1) },
            new Order { Id = 10, CustomerName = "James Clark", OrderDate = DateTime.Now }
        );

        // Seed OrderItems (10 OrderItems with references to Orders and Products)
        modelBuilder.Entity<OrderItem>().HasData(
            new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 1, UnitPrice = 699.99m }, // Smartphone for Alice
            new OrderItem { Id = 2, OrderId = 1, ProductId = 3, Quantity = 2, UnitPrice = 19.99m }, // Fiction Novels for Alice
            new OrderItem { Id = 3, OrderId = 2, ProductId = 2, Quantity = 1, UnitPrice = 1299.99m }, // Laptop for Bob
            new OrderItem { Id = 4, OrderId = 2, ProductId = 4, Quantity = 3, UnitPrice = 15.99m }, // T-Shirts for Bob
            new OrderItem { Id = 5, OrderId = 3, ProductId = 5, Quantity = 1, UnitPrice = 89.99m },  // Blender for Charlie
            new OrderItem { Id = 6, OrderId = 4, ProductId = 1, Quantity = 1, UnitPrice = 699.99m },  // Smartphone for David
            new OrderItem { Id = 7, OrderId = 5, ProductId = 2, Quantity = 2, UnitPrice = 1299.99m }, // Laptop for Eva
            new OrderItem { Id = 8, OrderId = 6, ProductId = 4, Quantity = 4, UnitPrice = 15.99m },  // T-Shirts for Fay
            new OrderItem { Id = 9, OrderId = 7, ProductId = 3, Quantity = 5, UnitPrice = 19.99m },  // Fiction Novels for George
            new OrderItem { Id = 10, OrderId = 8, ProductId = 5, Quantity = 3, UnitPrice = 89.99m }  // Blenders for Helen
        );
    }



}
