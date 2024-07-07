using Application.Interface.Repository;
using AutoMapper;
using Domain.Entity;
using Domain.Enum;
using Domain.Model.CustomerOrder;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class CustomerOrderRepository : GenericRepository<CustomerOrder>, ICustomerOrderRepository
    {
        private readonly PizzaFDbContext _context;
        private readonly IMapper _mapper;
        public CustomerOrderRepository(PizzaFDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerOrderModel> CreateCustomerOrder(CustomerOrderPostModel customerOrder)
        {
            try
            {
                var totalPrice = CalculateTotalPrice(customerOrder);
                string content = "";
                var customerOrderEntity = new CustomerOrder
                {
                    UserId = customerOrder.UserId,
                    TotalAmount = totalPrice,
                    OrderDate = DateTime.UtcNow,
                    Status = OrderStatus.Processing,
                };
                var order = await _context.CustomerOrders.AddAsync(customerOrderEntity);
                _context.SaveChanges();

                var customer = _context.Users.FirstOrDefault(x => x.UserId == customerOrder.UserId);
                if (customer != null)
                {
                    content ="Customer name is: "+ customer.Name + ", address is: " + customer.Address + ", phone number is: " + customer.Phone + ", has order: ";
                }

                if (customerOrder.customerDrinks != null)
                {
                    foreach (var item in customerOrder.customerDrinks)
                    {
                        var customerDrink = _mapper.Map<CustomerDrink>(item);
                        customerDrink.OrderId = order.Entity.OrderId;
                        await _context.CustomerDrinks.AddAsync(customerDrink);
                        _context.SaveChanges();

                        //find in for of Drink
                        var drink = _context.Drinks.FirstOrDefault(p => p.DrinkId == item.DrinkId);
                        if (drink != null)
                        {
                            content += item.Quantity + " " + drink.Name + ", ";
                        }
                    }
                }

                if (customerOrder.customerPizzas != null)
                {
                    foreach (var item in customerOrder.customerPizzas)
                    {
                        var customerPizza = _mapper.Map<CustomerPizza>(item);
                        customerPizza.OrderId = order.Entity.OrderId;
                        await _context.CustomerPizzas.AddAsync(customerPizza);
                        _context.SaveChanges();

                        //find in for of Pizza
                        var pizza = _context.Pizzas.FirstOrDefault(p => p.PizzaId == item.PizzaId);
                        if (pizza != null)
                        {
                            content += item.Quantity + " " + pizza.Name + ", ";
                        }
                        //find in for of Size
                        var size = _context.Sizes.FirstOrDefault(p => p.SizeId == item.SizeId);
                        if (size != null)
                        {
                            content += size.Name + ", ";
                        }
                        //find in for of Topping
                        var topping = _context.Toppings.FirstOrDefault(p => p.ToppingId == item.ToppingId);
                        if (topping != null)
                        {
                            content += topping.Name + ", ";
                        }
                    }
                }

                content += "total price: " + totalPrice;
                //create notification
                var notification = new Notification
                {
                    UserId = customerOrder.UserId,
                    Content = content,
                    ReceivedAt = DateTime.UtcNow,
                    Status = NotiStatus.Unread
                };
                await _context.Notifications.AddAsync(notification);
                _context.SaveChanges();

                return new CustomerOrderModel
                {
                    OrderId = order.Entity.OrderId,
                    UserId = order.Entity.UserId,
                    OrderDate = order.Entity.OrderDate,
                    customerDrinks = customerOrder.customerDrinks,
                    customerPizzas = customerOrder.customerPizzas,
                    TotalAmount = totalPrice
                };
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public async Task<BillModel> GetCustomerOrderById(int id)
        {
            try
            {
                var order = _context.CustomerOrders
                    .Include(o => o.User)
                    .Include(o => o.CustomerPizzas)
                        .ThenInclude(cp => cp.Pizza)
                    .Include(o => o.CustomerPizzas)
                        .ThenInclude(cp => cp.Size)
                    .Include(o => o.CustomerPizzas)
                        .ThenInclude(cp => cp.Topping)
                    .Include(o => o.CustomerDrinks)
                        .ThenInclude(cd => cd.Drink)
                    .FirstOrDefault(o => o.OrderId == id);

                if (order == null)
                {
                    return new BillModel();
                }

                var billDetails = order.CustomerPizzas.Select(pizza => new BillDetailModel
                {
                    Name = pizza.Pizza.Name + " " + pizza.Size.Name + " " + pizza.Topping.Name,
                    Quantity = pizza.Quantity,
                    Price = pizza.Price
                })
                .Union(order.CustomerDrinks.Select(drink => new BillDetailModel
                {
                    Name = drink.Drink.Name,
                    Quantity = drink.Quantity,
                    Price = drink.Price
                }))
                .ToList();

                var bill = new BillModel
                {
                    UserName = order.User.Name,
                    DateTime = order.OrderDate,
                    TotalPrice = order.TotalAmount,
                    BillDetails = billDetails
                };

                return bill;

            }
            catch (Exception ex)
            {
                return new BillModel();
            }
        }

        public async Task<IEnumerable<CusOrderHistoryModel>> GetHistoryOrderByUserId(int userId)
        {
            var listOrder = _context.CustomerOrders
                .Where(p => p.UserId == userId && p.Status == OrderStatus.Completed)
                .Include(p => p.CustomerPizzas)
                .ThenInclude(cp => cp.Pizza)
                .Select(order => new CusOrderHistoryModel
                {
                    OrderId = order.OrderId,
                    TotalAmount = order.TotalAmount,
                    OrderDate = order.OrderDate,
                    Image = order.CustomerPizzas.Select(cp => cp.Pizza.Image).FirstOrDefault()
                })
                .ToList();

            return listOrder;
        }

        private decimal CalculateTotalPrice(CustomerOrderPostModel customerOrder)
        {
            decimal totalPrice = 0;
            if (customerOrder.customerDrinks != null)
            {
                foreach (var item in customerOrder.customerDrinks)
                {
                    totalPrice += item.Price * item.Quantity;
                }
            }
            if (customerOrder.customerPizzas != null)
            {
                foreach (var item in customerOrder.customerPizzas)
                {
                    totalPrice += item.Price * item.Quantity;
                }
            }
            return totalPrice;
        }
    }
}
