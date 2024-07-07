using Application.Interface.Repository;
using Domain.Entity;
using Domain.Model.Notification;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class NotificationRepository: GenericRepository<Notification>, INotificationRepository
    {
        private readonly PizzaFDbContext _context;
        public NotificationRepository(PizzaFDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Notification> createNotification(NotificationPostModel model)
        {
            string content = "";
            var customer = _context.Users.FirstOrDefault(x => x.UserId == model.UserId);
            if (customer != null)
            {
                content = customer.Name + ", address is: " + customer.Address + " phone number is: " + customer.Phone + " has order: ";
            }
            content += model.Content;
            var notification = new Notification
            {
                UserId = model.UserId,
                Content = content,
                ReceivedAt = DateTime.UtcNow
            };
            var result = await _context.Notifications.AddAsync(notification);
            _context.SaveChanges();

            return result.Entity;

        }

        public async Task<IEnumerable<Notification>> getAllNotification()
        {
            var listNotification = await _context.Notifications.OrderBy(p=>p.Status).ToListAsync();
            return listNotification;

        }
    }
}
