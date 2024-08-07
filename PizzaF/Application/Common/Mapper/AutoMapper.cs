﻿using AutoMapper;
using Domain.Entity;
using Domain.Model.CustomerDrink;
using Domain.Model.CustomerOrder;
using Domain.Model.CustomerPizza;
using Domain.Model.Drink;
using Domain.Model.Messages;
using Domain.Model.Notification;
using Domain.Model.Pizza;
using Domain.Model.Size;
using Domain.Model.Topping;
using Domain.Model.User;
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
            CreateMap<Pizza, PizzaPutModel>().ReverseMap();

            //Drink
            CreateMap<Drink, DrinkModel>().ReverseMap();
            CreateMap<Drink, DrinkPostModel>().ReverseMap();
            CreateMap<Drink, DrinkPutModel>().ReverseMap();

            //Size
            CreateMap<Size, SizeModel>().ReverseMap();
            CreateMap<Size, SizePostModel>().ReverseMap();
            CreateMap<Size, SizePutModel>().ReverseMap();

            //Topping
            CreateMap<Topping, ToppingModel>().ReverseMap();
            CreateMap<Topping, ToppingPostModel>().ReverseMap();
            CreateMap<Topping, ToppingPutModel>().ReverseMap();

            //CustomerPizza
            CreateMap<CustomerPizza, CustomerPizzaModel>().ReverseMap();
            CreateMap<CustomerPizza, CustomerPizzaPostModel>().ReverseMap();
            CreateMap<CustomerPizza, CustomerPizzaPutModel>().ReverseMap();

            //CustomerDrink
            CreateMap<CustomerDrink, CustomerDrinkModel>().ReverseMap();
            CreateMap<CustomerDrink, CustomerDrinkPostModel>().ReverseMap();
            CreateMap<CustomerDrink, CustomerDrinkPutModel>().ReverseMap();

            //CustomerOrder
            CreateMap<CustomerOrder, CusOrderHistoryModel>().ReverseMap();

            //User
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, LoginModel>().ReverseMap();
            CreateMap<User, RegisterModel>().ReverseMap();
            CreateMap<User, ProfileModel>().ReverseMap();
            CreateMap<User, ProfilePutModel>().ReverseMap();

            //Notification
            CreateMap<Notification, NotificationModel>().ReverseMap();

            //Message
            CreateMap<Message, MessageModel>().ReverseMap();

        }
    }
}
