using Application.Interface.Repository;
using Domain.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class MessageRepository: GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(PizzaFDbContext context) : base(context)
        {

        }
    }
}
