using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        ApplicationDbContext _context;
        public UserService(ApplicationDbContext dbContext) => _context = dbContext;

        public Task<User> GetUser()
        {
            var usersOrders = _context.Orders.GroupBy(o => o.UserId).Select(uo => new { UserId = uo.Key, OrdersCount = uo.Count()});
            int userMaxOrdersCount = usersOrders.Max(o => o.OrdersCount);
            int userId = usersOrders.First(uo => uo.OrdersCount == userMaxOrdersCount).UserId;
            return _context.Users.FirstAsync(u => u.Id == userId);
        }

        public Task<List<User>> GetUsers()
        {
           return _context.Users.Where(u => u.Status == UserStatus.Inactive).ToListAsync();
        }
    }
}
