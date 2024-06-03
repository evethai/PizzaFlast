using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entity;
using Domain.Model;
using Domain.Model.Pizza;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Service
{
    public class PizzaService : IPizzaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PizzaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CreatePizzaAsync(PizzaPostModel model)
        {
            var pizza = _mapper.Map<Pizza>(model);
            var result = await _unitOfWork.PizzaRepository.AddAsync(pizza);
            _unitOfWork.Save();
            return new ResponseModel
            {
                IsSuccess = true,
                Message = "Pizza created successfully.",
                Data = pizza
            };
        }

        public async Task<PizzaResponseModel> GetListPizzaAsync(PizzaSearchModel searchModel)
        {
            var (filter, orderBy) = _unitOfWork.PizzaRepository.BuildFilterAndOrderBy(searchModel);
            var pizzas = await _unitOfWork.PizzaRepository.GetByConditionAsync(filter, orderBy, pageIndex: searchModel.currentPage, pageSize: searchModel.pageSize);
            var total = await _unitOfWork.PizzaRepository.CountAsync(filter);
            var listPizza = _mapper.Map<List<PizzaModel>>(pizzas);
            return new PizzaResponseModel
            {
                total = total,
                currentPage = searchModel.currentPage,
                pizzas = listPizza,
            };
        }

        public async Task<PizzaModel> GetPizzaByIdAsync(int pizzaId)
        {
            var pizza = await _unitOfWork.PizzaRepository.GetByIdAsync(pizzaId);
            var result = _mapper.Map<PizzaModel>(pizza);
            return result;
        }

        public async Task<ResponseModel> UpdatePizzaAsync(PizzaPutModel model)
        {
            var pizza = _mapper.Map<Pizza>(model);
            var result = await _unitOfWork.PizzaRepository.UpdateAsync(pizza);
            _unitOfWork.Save();
            return new ResponseModel
            {
                IsSuccess = true,
                Message = "Pizza updated successfully.",
                Data = pizza
            };
        }
    }
}
