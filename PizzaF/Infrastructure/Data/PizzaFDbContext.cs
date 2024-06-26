﻿using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class PizzaFDbContext : DbContext
    {
        public PizzaFDbContext(DbContextOptions<PizzaFDbContext> options) : base(options)
        {
        }

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<CustomerDrink> CustomerDrinks { get; set; }
        public DbSet<CustomerPizza> CustomerPizzas { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerDrink>()
                .HasOne(cd => cd.CustomerOrder)
                .WithMany(co => co.CustomerDrinks)
                .HasForeignKey(cd => cd.OrderId);

            modelBuilder.Entity<CustomerPizza>()
                .HasOne(cp => cp.CustomerOrder)
                .WithMany(co => co.CustomerPizzas)
                .HasForeignKey(cp => cp.OrderId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
