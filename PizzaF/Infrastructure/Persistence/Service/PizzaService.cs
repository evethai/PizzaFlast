using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Model;
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
        public async Task<PizzaResponseModel> GetListPizzaAsync(PizzaSearchModel searchModel)
        {
            var (filter, orderBy) = _unitOfWork.PizzaRepository.BuildFilterAndOrderBy(searchModel);
            var pizzas = await _unitOfWork.PizzaRepository.GetByConditionAsync(filter, orderBy, pageIndex: searchModel.currentPage, pageSize: searchModel.pageSize);
            var total = await _unitOfWork.PizzaRepository.CountAsync(filter);

            var listPizza = _mapper.Map<List<PizzaListModel>>(pizzas);

            return new PizzaResponseModel
            {
                total = total,
                currentPage = searchModel.currentPage,
                pizzas = listPizza,
            };
        }
    }
}
