using AutoMapper;
using Domain.Entity;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Pizza, PizzaListModel>().ReverseMap();
        }
    }
}
