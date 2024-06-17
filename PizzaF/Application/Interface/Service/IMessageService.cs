using Domain.Model.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface IMessageService
    {
        Task SendMessage(MessageModel model);
    }
}
