using Domain.Model;
using Domain.Model.Size;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface ISizeService
    {
        Task<SizeResponseModel> GetSizes();
        Task<SizeModel> GetSizeById(int id);
        Task<ResponseModel> CreateSize(SizePostModel sizeModel);
        Task<ResponseModel> UpdateSize(SizePutModel sizeModel);
        Task<ResponseModel> DeleteSize(int id);

    }
}
