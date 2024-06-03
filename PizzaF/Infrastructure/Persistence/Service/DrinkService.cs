using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Model;
using Domain.Model.Drink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Service
{
    public class DrinkService : IDrinkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DrinkService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseModel> CreateDrinkAsync(DrinkPostModel model)
        {
            var drink = _mapper.Map<Domain.Entity.Drink>(model);
            var result = await _unitOfWork.DrinkRepository.AddAsync(drink);
            _unitOfWork.Save();
            return new ResponseModel
            {
                IsSuccess = true,
                Message = "Drink created successfully.",
                Data = drink
            };
        }

        public async Task<DrinkModel> GetDrinkByIdAsync(int DrinkId)
        {
            var drink = await _unitOfWork.DrinkRepository.GetByIdAsync(DrinkId);
            var result = _mapper.Map<DrinkModel>(drink);
            return result;
        }

        public async Task<DrinkResponseModel> GetListDrinkAsync(DrinkSearchModel searchModel)
        {
            var (filter, orderBy) = _unitOfWork.DrinkRepository.BuildFilterAndOrderBy(searchModel);
            var drinks = await _unitOfWork.DrinkRepository.GetByConditionAsync(filter, orderBy, pageIndex: searchModel.currentPage, pageSize: searchModel.pageSize);
            var total = await _unitOfWork.DrinkRepository.CountAsync(filter);
            var listDrink = _mapper.Map<List<DrinkModel>>(drinks);
            
            return new DrinkResponseModel
            {
                total = total,
                currentPage = searchModel.currentPage,
                drinks = listDrink,
            };
        }

        public async Task<ResponseModel> UpdateDrinkAsync(DrinkPutModel model)
        {
            var drink = _mapper.Map<Domain.Entity.Drink>(model);
            var result = await _unitOfWork.DrinkRepository.UpdateAsync(drink);
            _unitOfWork.Save();
            return new ResponseModel
            {
                IsSuccess = true,
                Message = "Drink updated successfully.",
                Data = drink
            };
        }
    }
}
