using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entity;
using Domain.Model;
using Domain.Model.CustomerPizza;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Service
{
    public class CustomerPizzaService : ICustomerPizzaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerPizzaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CreateCustomerPizza(CustomerPizzaPostModel customerPizzaModel)
        {
            var customerPizza = _mapper.Map<CustomerPizza>(customerPizzaModel);
            await _unitOfWork.CustomerPizzaRepository.AddAsync(customerPizza);
            _unitOfWork.Save();

            return new ResponseModel
            {
                Message = "Customer Pizza created successfully",
                IsSuccess = true,
                Data = customerPizza
            };

        }

        public async Task<CustomerPizzaModel> GetCustomerPizzaById(int id)
        {
            var customerPizza = await _unitOfWork.CustomerPizzaRepository.GetByIdAsync(id);
            return _mapper.Map<CustomerPizzaModel>(customerPizza);
        }

        public async Task<CustomerPizzaResponseModel> GetCustomerPizzasByOrderId(int id)
        {
            var list = await _unitOfWork.CustomerPizzaRepository.GetByOrderId(id);
            var result = _mapper.Map<List<CustomerPizzaModel>>(list);

            return new CustomerPizzaResponseModel
            {
                customerPizzas = result
            };
        }

        public async Task<ResponseModel> UpdateCustomerPizza(CustomerPizzaPutModel customerPizzaModel)
        {
            var customerPizza = _mapper.Map<CustomerPizza>(customerPizzaModel);
            await _unitOfWork.CustomerPizzaRepository.UpdateAsync(customerPizza);
            _unitOfWork.Save();

            return new ResponseModel
            {
                Message = "Customer Pizza updated successfully",
                IsSuccess = true,
                Data = customerPizza
            };
        }
    }
}
