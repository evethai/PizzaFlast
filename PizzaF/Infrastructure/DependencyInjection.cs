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

            //Declare Service
            services.AddScoped<IPizzaService, PizzaService>();
            services.AddScoped<IDrinkService, DrinkService>();

            return services;
        }
    }
}
