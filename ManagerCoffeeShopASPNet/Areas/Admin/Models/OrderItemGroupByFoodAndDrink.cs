using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Areas.Admin.Models
{
    public class OrderItemGroupByFoodAndDrink
    {
        public FoodAndDrink FoodAndDrink;
        public int Quantity;
        public List<OrderItem> OrderItems;
    }
}