using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entity;
using Domain.Model;
using Domain.Model.CustomerDrink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Service
{
    public class CustomerDrinkService : ICustomerDrinkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerDrinkService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<ResponseModel> CreateCustomerDrink(CustomerDrinkPostModel customerDrinkModel)
        {
            var customerDrink = _mapper.Map<CustomerDrink>(customerDrinkModel);
            await _unitOfWork.CustomerDrinkRepository.AddAsync(customerDrink);
            _unitOfWork.Save();

            return new ResponseModel
            {
                Message = "Customer Drink Created Successfully",
                IsSuccess = true,
                Data = customerDrink
            };
        }

        public async Task<CustomerDrinkModel> GetCustomerDrinkById(int id)
        {
            var customerDrink = await _unitOfWork.CustomerDrinkRepository.GetByIdAsync(id);
            return _mapper.Map<CustomerDrinkModel>(customerDrink);
        }

        public async Task<CustomerDrinkResponseModel> GetCustomerDrinkByOrderId(int id)
        {
            var list = await _unitOfWork.CustomerDrinkRepository.GetListByOrderId(id);
            var result = _mapper.Map<List<CustomerDrinkModel>>(list);

            return new CustomerDrinkResponseModel
            {
                customerDrinks = result
            };
        }

        public async Task<ResponseModel> UpdateCustomerDrink(CustomerDrinkPutModel customerDrinkModel)
        {
            var customerDrink = _mapper.Map<CustomerDrink>(customerDrinkModel);
            await _unitOfWork.CustomerDrinkRepository.UpdateAsync(customerDrink);
            _unitOfWork.Save();

            return new ResponseModel
            {
                Message = "Customer Drink Updated Successfully",
                IsSuccess = true,
                Data = customerDrink
            };
        }
    }
}
