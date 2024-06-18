﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Payment
{
    public class PaymentModel
    {
        public Guid Id { get; set; }

        public Guid? OrderId { get; set; }

        public double? Amount { get; set; }

        public string? Method { get; set; }

        public string? Status { get; set; }

        //  public virtual Order? Order { get; set; }
    }

}
