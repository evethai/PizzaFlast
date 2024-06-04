using Application.Interface;
using Application.Interface.Repository;
using Application.Interface.Service;
using Infrastructure.Data;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repository;
using Infrastructure.Persistence.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<PizzaFDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            //Declare UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Declare Repository
            services.AddScoped<IPizzaRepository,PizzaRepository>();
            services.AddScoped<IDrinkRepository, DrinkRepository>();
            services.AddScoped<ISizeRepository, SizeRepository>();
            services.AddScoped<IToppingRepository, ToppingRepository>();
            services.AddScoped<ICustomerPizzaRepository, CustomerPizzaRepository>();
            services.AddScoped<ICustomerDrinkRepository, CustomerDrinkRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            //Declare Service
            services.AddScoped<IPizzaService, PizzaService>();
            services.AddScoped<IDrinkService, DrinkService>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IToppingService, ToppingService>();
            services.AddScoped<ICustomerPizzaService, CustomerPizzaService>();
            services.AddScoped<ICustomerDrinkService, CustomerDrinkService>();
            services.AddScoped<IUserService, UserService>();



            return services;
        }
    }
}
