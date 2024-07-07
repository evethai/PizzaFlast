using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Enum;
using Domain.Model;
using Domain.Model.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Service
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NotificationModel>> getAllNotification()
        {
            var list = await _unitOfWork.NotificationRepository.getAllNotification();
            var result = _mapper.Map<IEnumerable<NotificationModel>>(list);
            return result;
        }

        public async Task<NotificationModel> getNotificationById(int id)
        {
            var noti = await _unitOfWork.NotificationRepository.GetByIdAsync(id);
            if (noti == null)
            {
                return null;
            }
            var result = _mapper.Map<NotificationModel>(noti);
            return result;
        }

        public async Task<ResponseModel> SendNotificationAsync(NotificationPostModel model)
        {
            var result = await _unitOfWork.NotificationRepository.createNotification(model);
            return new ResponseModel
            {
                Message = "Notification sent successfully",
                IsSuccess = true,
                Data = result
            };
        }

        public async Task<ResponseModel> updateStatusNotification(int id)
        {
            var noti = await _unitOfWork.NotificationRepository.GetByIdAsync(id);
            if (noti == null)
            {
                return new ResponseModel
                {
                    Message = "Notification not found",
                    IsSuccess = false
                };
            }
            noti.Status = NotiStatus.Read;
            await _unitOfWork.NotificationRepository.UpdateAsync(noti);
            _unitOfWork.Save();
            return new ResponseModel
            {
                Message = "Notification updated successfully",
                IsSuccess = true
            };

        }
    }
}
