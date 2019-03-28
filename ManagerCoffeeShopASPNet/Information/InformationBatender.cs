using ManagerCoffeeShopASPNet.DAO;
using ManagerCoffeeShopASPNet.DAOImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Information
{
    public class InformationBatender
    {
        private OrderDAO _orderDAO;
        private OrderItemDAO _orderItemDAO;
        private RecipeDAO _recipeDAO;
        private RecipeDetailDAO _recipeDetailDAO;
        private FoodAndDrinkDAO _foodAndDrinkDAO;
        public InformationBatender()
        {
            this._orderDAO = (OrderDAO)new OrderDAOImpl();
            this._orderItemDAO = (OrderItemDAO)new OrderItemDAOImpl();
            this._recipeDAO = (RecipeDAO)new RecipeDAOImpl();
            this._recipeDetailDAO = (RecipeDetailDAO)new RecipeDetailDAOImpl();
            this._foodAndDrinkDAO = (FoodAndDrinkDAO)new FoodAndDrinkDAOImpl();

        }
        public IEnumerable<Order> GetAllOrderByStatus(string status)
        {
            return this._orderDAO.GetAllOrderByStatus(status);
        }
        public IEnumerable<OrderItem> GetAllOrderItemByOrderID(int OrderID)
        {
            return this._orderItemDAO.GetAllOrderItemByOrderID(OrderID);
        }
        public bool UpdateStatus(int orderItemID, string status)
        {
            return this._orderItemDAO.UpdateOrderItemStatus(orderItemID, status);
        }
        public IEnumerable<OrderItem> GetAllOrderItemByOrderIDAndStatus(int OrderID, string Status)
        {
            return this._orderItemDAO.GetAllOrderItemByOrderIDAndStatus(OrderID, Status);
        }
        public bool UpdateOrderStatus(int OrderID, string Status)
        {
            return this._orderDAO.UpdateOrderStatus(OrderID, Status);
        }
        public IEnumerable<OrderItem> GetAllOrderItemByStatus(string status)
        {
            return this._orderItemDAO.GetAllOrderItemByStatus(status);
        }
        public Recipe GetRecipeByFDID(int FDID)
        {
            return this._recipeDAO.GetRecipeByFoodDrinkID(FDID);
        }
        public IEnumerable<RecipeDetail> GetAllRecipeDetailByRecipeID(int RecipeID)
        {
            return this._recipeDetailDAO.GetAllRecipeDetailByRecipeID(RecipeID);
        }
        public IEnumerable<FoodAndDrink> GetAllFoodAndDrink()
        {
            return this._foodAndDrinkDAO.GetAllFoodAndDrink();
        }
        public bool InsertFoodAndDrink(string Name, string Desc,
            string ImagePath, string Size, string Type, double UnitPrice, string Currency)
        {
            return this._foodAndDrinkDAO.InsertFoodAndDrink(Name, Desc, ImagePath, Size, Type, UnitPrice, Currency);
        }
        public FoodAndDrink GetFoodAndDrinkByFDID(int FDID)
        {
            return this._foodAndDrinkDAO.GetFoodAndDrinkByID(FDID);
        }
    }
}