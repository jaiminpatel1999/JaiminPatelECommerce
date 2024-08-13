using JaiminPatelECommerce.Data;
using JaiminPatelECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace JaiminPatelECommerce.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Method to get all available products
        public async Task<List<Product>> GetAvailableProducts()
        {
            return await _context.Products
                                 .Where(p => p.IsAvailable && p.Stock > 0)
                                 .ToListAsync();
        }

        public async Task<List<Product>> GetTopProducts()
        {
            return await _context.Products
                                 .Where(p => p.IsAvailable && p.Stock > 0)
                                 .Take(3)
                                 .ToListAsync();
        }


        // Method to get a specific product by ID
        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products
                                 .FirstOrDefaultAsync(p => p.ProductId == id && p.IsAvailable && p.Stock > 0);
        }

        // Method to add a new product (admin functionality)
        public async Task AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        // Method to update an existing product (admin functionality)
        public async Task UpdateProduct(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.ProductId);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
                existingProduct.ImageUrl = product.ImageUrl;
                existingProduct.IsAvailable = product.IsAvailable;

                await _context.SaveChangesAsync();
            }
        }

        // Method to delete a product (admin functionality)
        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
