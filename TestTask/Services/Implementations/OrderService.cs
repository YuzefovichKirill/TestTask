using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        ApplicationDbContext _context;
        public OrderService(ApplicationDbContext dbContext) => _context = dbContext;

        public Task<Order> GetOrder() 
        {
            int total = _context.Orders.MaxAsync(o => o.Price * o.Quantity).Result;
            return _context.Orders.FirstAsync(o => o.Price * o.Quantity == total);
        }

        public Task<List<Order>> GetOrders()
        {
           return _context.Orders.Where(o => o.Quantity > 10).ToListAsync();
        }
    }
}
