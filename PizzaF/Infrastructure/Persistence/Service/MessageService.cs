using Application.Interface;
using Application.Interface.Repository;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entity;
using Domain.Model.Messages;
using Microsoft.Extensions.Configuration;
using PusherServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Service
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public MessageService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
        }

        public async Task SendMessage(MessageModel model)
        {
            var api_id = _config.GetSection("Pusher:api_id").Value;
            var key = _config.GetSection("Pusher:key").Value;
            var secret = _config.GetSection("Pusher:secret").Value;
            var cluster = _config.GetSection("Pusher:cluster").Value;

            var options = new PusherOptions
            {
                Cluster = cluster,
                Encrypted = true
            };

            var pusher = new Pusher(
              api_id,
              key,
              secret,
              options);

            //var message = _mapper.Map<Message>(model);
            //message.SentAt = DateTime.UtcNow;
            //await _unitOfWork.MessageRepository.AddAsync(message);
            //_unitOfWork.Save();


            var result = await pusher.TriggerAsync(
              "chat",
              "message",
              new { 
                  sender = model.SenderId,
                  receiver = model.ReceiverId,
                  message = model.Content,
              });
        }
    }
}
