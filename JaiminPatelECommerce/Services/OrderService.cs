using JaiminPatelECommerce.Data;
using JaiminPatelECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace JaiminPatelECommerce.Services
{
    public class OrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Method to create a new order
        public async Task<Order> CreateOrder(string userId, List<CartItem> cartItems, decimal totalAmount)
        {
            if (cartItems == null || !cartItems.Any())
                throw new ArgumentException("Cart items cannot be null or empty");

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                OrderItems = cartItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        // Method to get all orders for a specific user
        public async Task<List<Order>> GetOrdersByUserId(string userId)
        {
            return await _context.Orders
                                 .Include(o => o.OrderItems)
                                 .Where(o => o.UserId == userId)
                                 .OrderByDescending(o => o.OrderDate)
                                 .ToListAsync();
        }

        // Method to get a specific order by its ID
        public async Task<Order> GetOrderById(int orderId)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        // Method to get all orders (admin functionality)
        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders
                .Include(o => o.Customer)
                                 .Include(o => o.OrderItems)
                                 .OrderByDescending(o => o.OrderDate)
                                 .ToListAsync();
        }

    }
}
