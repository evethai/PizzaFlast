using Domain.Model;
using Domain.Model.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface INotificationService
    {
        Task<ResponseModel> SendNotificationAsync(NotificationPostModel model);
        Task<NotificationModel> getNotificationById(int id);
        Task<IEnumerable<NotificationModel>> getAllNotification();
        Task<ResponseModel> updateStatusNotification(int id);
    }
}
