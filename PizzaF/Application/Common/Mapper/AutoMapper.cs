using AutoMapper;
using Domain.Entity;
using Domain.Model.Drink;
using Domain.Model.Pizza;
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
            //Pizza
            CreateMap<Pizza, PizzaModel>().ReverseMap();
            CreateMap<Pizza, PizzaPostModel>().ReverseMap();
            CreateMap<Pizza, PizzaPushModel>().ReverseMap();

            //Drink
            CreateMap<Drink, DrinkModel>().ReverseMap();
            CreateMap<Drink, DrinkPostModel>().ReverseMap();
            CreateMap<Drink, DrinkPushModel>().ReverseMap();
        }
    }
}
