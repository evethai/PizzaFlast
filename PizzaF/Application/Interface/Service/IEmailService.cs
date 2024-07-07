﻿using Domain.Model.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface IEmailService
    {
        Task<bool> SendEmail(int userId);
    }
}
