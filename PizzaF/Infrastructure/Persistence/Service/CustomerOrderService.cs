﻿using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Model;
using Domain.Model.CustomerOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Service
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomerOrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CreateOrder(CustomerOrderPostModel model)
        {
            var order = await _unitOfWork.CustomerOrderRepository.CreateCustomerOrder(model);
            return new ResponseModel
            {
                Message = "Order created successfully",
                IsSuccess = true,
                Data = order
            };
        }

        public Task<BillModel> GetBillByOrderId(int id)
        {
            var bill = _unitOfWork.CustomerOrderRepository.GetCustomerOrderById(id);
            return bill;
        }
    }
}