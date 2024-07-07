using Domain.Entity;
using Domain.Model.Notification;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        Task<Notification> createNotification(NotificationPostModel model);
        Task<IEnumerable<Notification>> getAllNotification();
    }
}
