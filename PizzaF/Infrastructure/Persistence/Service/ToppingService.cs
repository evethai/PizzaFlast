using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entity;
using Domain.Model;
using Domain.Model.Size;
using Domain.Model.Topping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Service
{
    public class ToppingService : IToppingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ToppingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<ResponseModel> CreateTopping(ToppingPostModel toppingModel)
        {
            var topping = _mapper.Map<Topping>(toppingModel);
            var result = await _unitOfWork.ToppingRepository.AddAsync(topping);
            _unitOfWork.Save();

            return new ResponseModel
            {
                Message = "Topping Created Successfully",
                IsSuccess = true,
                Data = topping
            };
        }

        public async Task<ResponseModel> DeleteTopping(int id)
        {
            var topping = await _unitOfWork.ToppingRepository.GetByIdAsync(id);
            var result = await _unitOfWork.ToppingRepository.DeleteAsync(topping);
            _unitOfWork.Save();

            return new ResponseModel
            {
                Message = "Topping Deleted Successfully",
                IsSuccess = true,
            };
        }

        public async Task<ToppingModel> GetToppingById(int id)
        {
           var topping = await _unitOfWork.ToppingRepository.GetByIdAsync(id);
            return _mapper.Map<ToppingModel>(topping);
        }

        public  async Task<ToppingResponseModel> GetToppings()
        {
            var toppings = await _unitOfWork.ToppingRepository.GetAllAsync();
            var result = _mapper.Map<List<ToppingModel>>(toppings);

            return new ToppingResponseModel
            {
                toppings = result
            };
        }

        public async Task<ResponseModel> UpdateTopping(ToppingPutModel toppingModel)
        {
            var topping = _mapper.Map<Topping>(toppingModel);
            var result = await _unitOfWork.ToppingRepository.UpdateAsync(topping);
            _unitOfWork.Save();

            return new ResponseModel
            {
                Message = "Topping Updated Successfully",
                IsSuccess = true,
                Data = topping
            };
        }
    }
}
