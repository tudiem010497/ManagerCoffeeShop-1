﻿using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class OrderItemDAOImpl:OrderItemDAO
    {
        CoffeeShopDBDataContext context;
        public OrderItemDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public int GetLastID()
        {
            int id = (from orderItem
                      in context.OrderItems
                      orderby orderItem.OrderItemID descending
                      select orderItem.OrderItemID
                      ).FirstOrDefault();
            return id;
        }
        public void InsertOrderItem(int OrderID, int FDID,int Quantity, string Desc, string Status)
        {
            try
            {
                int id = GetLastID() + 1;
                OrderItem orderitem = new OrderItem();
                orderitem.OrderItemID = id;
                orderitem.OrderID = OrderID;
                orderitem.FDID = FDID;
                orderitem.Quantity = Quantity;
                orderitem.Desc = Desc;
                orderitem.Status = Status;
                context.ExecuteCommand("SET IDENTITY_INSERT dbo.OrderItem ON");
                context.OrderItems.InsertOnSubmit(orderitem);
                context.SubmitChanges();
            }
            catch(Exception ex)
            {
                throw new Exception("Error Inser To OrderItem" + ex.Message);
            }
            
        }
    }
}