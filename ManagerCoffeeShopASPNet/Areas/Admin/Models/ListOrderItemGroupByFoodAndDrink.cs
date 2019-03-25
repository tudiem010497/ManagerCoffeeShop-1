using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Areas.Admin.Models
{
    public class ListOrderItemGroupByFoodAndDrink
    {
        public List<OrderItemGroupByFoodAndDrink> list;
        public ListOrderItemGroupByFoodAndDrink()
        {
            this.list = new List<OrderItemGroupByFoodAndDrink>();
        }
        public int FindIndexFoodAndDrinkInListGroup(int FDID)
        {
            int count = 0;
            foreach (var item in list)
            {
                if (item.FoodAndDrink.FDID == FDID)
                    return count;
                count++;
            }
            return -1;
        }
    }
}