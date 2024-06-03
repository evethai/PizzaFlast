using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entity;
using Domain.Model;
using Domain.Model.Pizza;
using Domain.Model.Size;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Service
{
    public class SizeService : ISizeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SizeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CreateSize(SizePostModel sizeModel)
        {
            var size = _mapper.Map<Size>(sizeModel);
            var result = await _unitOfWork.SizeRepository.AddAsync(size);
            _unitOfWork.Save();

            return new ResponseModel
            {
                IsSuccess = true,
                Message = "Size created successfully.",
                Data = size
            };

        }

        public async Task<ResponseModel> DeleteSize(int id)
        {
            var size = await _unitOfWork.SizeRepository.GetByIdAsync(id);
            var result = _unitOfWork.SizeRepository.DeleteAsync(size);
            _unitOfWork.Save();

            return new ResponseModel
            {
                IsSuccess = true,
                Message = "Size deleted successfully.",
            };
        }

        public async Task<SizeModel> GetSizeById(int id)
        {
            var size = await _unitOfWork.SizeRepository.GetByIdAsync(id);
            var result = _mapper.Map<SizeModel>(size);
            return result;
        }

        public async Task<SizeResponseModel> GetSizes()
        {
            var sizes = await _unitOfWork.SizeRepository.GetAllAsync();
            var result = _mapper.Map<List<SizeModel>>(sizes);

            return new SizeResponseModel
            {
                sizes = result
            };
        }

        public async Task<ResponseModel> UpdateSize(SizePutModel sizeModel)
        {
            var size = _mapper.Map<Size>(sizeModel);
            var result = await _unitOfWork.SizeRepository.UpdateAsync(size);
            _unitOfWork.Save();

            return new ResponseModel
            {
                IsSuccess = true,
                Message = "Size updated successfully.",
                Data = size
            };
        }

        
    }
}
