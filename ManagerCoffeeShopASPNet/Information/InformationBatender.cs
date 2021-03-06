﻿using ManagerCoffeeShopASPNet.DAO;
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
        private IngredientDAO _ingredientDAO;
        private IngredientMessageDAO _ingredientMessageDAO;
        private IngreExchangeDAO _ingreExchangeDAO;
        public InformationBatender()
        {
            this._orderDAO = (OrderDAO)new OrderDAOImpl();
            this._orderItemDAO = (OrderItemDAO)new OrderItemDAOImpl();
            this._recipeDAO = (RecipeDAO)new RecipeDAOImpl();
            this._recipeDetailDAO = (RecipeDetailDAO)new RecipeDetailDAOImpl();
            this._foodAndDrinkDAO = (FoodAndDrinkDAO)new FoodAndDrinkDAOImpl();
            this._ingredientDAO = (IngredientDAO)new IngredientDAOImpl();
            this._ingredientMessageDAO = (IngredientMessageDAO)new IngredientMessageDAOImpl();
            this._ingreExchangeDAO = (IngreExchangeDAO)new IngreExchangeDAOImpl();
        }
        public bool UpdateIngredient(Ingredient ingredient)
        {
            return this._ingredientDAO.UpdateIngredient(ingredient);
        }
        public OrderItem GetOrderItemByOrderItemID(int OrderItemID)
        {
            return this._orderItemDAO.GetOrderItemByID(OrderItemID);
        }
        public IEnumerable<Order> GetOrderByOrderID(int OrderID)
        {
            return this._orderDAO.GetOrderByOrderID(OrderID);
        }
        public IngreExchange GetIngreExchangeByRecipeDetailID(int RecipeDetailID)
        {
            return this._ingreExchangeDAO.GetIngreExchangeByRecipeDetailID(RecipeDetailID);
        }
        public bool EditIngreExchange(IngreExchange exchange)
        {
            return this._ingreExchangeDAO.EditIngreExchange(exchange);
        }
        public int GetLastRecipeDetailID()
        {
            return this._recipeDetailDAO.GetLastRecipeDetailID();
        }
        public bool InsertIngreExchange(IngreExchange ingreExchange)
        {
            return this._ingreExchangeDAO.InsertIngreExchange(ingreExchange);
        }
        public IngreExchange GetIngreExchangeByIngreExchangeID(int IngreExchangeID)
        {
            return this._ingreExchangeDAO.GetIngreExchangeByIngreExchangeID(IngreExchangeID);
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
            int num = this._recipeDetailDAO.GetAllRecipeDetailByRecipeID(RecipeID).Count();
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
        //public FoodAndDrink GetFoodAndDrink(string search)
        //{
        //    return this._foodAndDrinkDAO.GetFoodAndDrink(search);
        //}
        public bool DeleteFoodAndDrinkByFDID(int FDID)
        {
            return this._foodAndDrinkDAO.DeleteFoodAndDrinkByFDID(FDID);
        }
        public bool EditFoodAndDrink(FoodAndDrink foodAndDrink)
        {
            return this._foodAndDrinkDAO.EditFoodAndDrink(foodAndDrink);
        }
        public bool InsertRecipe(int FDID)
        {
            return this._recipeDAO.InsertRecipe(FDID);
        }
        public bool InsertRecipeDetail(int RecID, int Step, int IngreID, double Amount, string Unit, string Desc)
        {
            return this._recipeDetailDAO.InsertRecipeDetail(RecID, Step, IngreID, Amount, Unit, Desc);
        }
        public IEnumerable<Ingredient> GetAllIngredient()
        {
            return this._ingredientDAO.GetAllIngredient();
        }
        public int CountRecipeDetailByRecipeID(int RecipeID)
        {
            return this._recipeDetailDAO.CountRecipeDetailByRecipeID(RecipeID);
        }
        public bool InsertIngredientMessage(int IngreID, int EmployeeID, double Amount, string Unit, string SendMessage)
        {
            return this._ingredientMessageDAO.InsertIngredientMessage(IngreID, EmployeeID, Amount, Unit, SendMessage);
        }
        public RecipeDetail GetRecipeDetailByRecipeDetailID(int RecipeDetailID)
        {
            return this._recipeDetailDAO.GetRecipeDetailByRecipeDetailID(RecipeDetailID);
        }
        public Ingredient GetIngredientByIngreID(int IngreID)
        {
            return this._ingredientDAO.GetIngredientByIngreID(IngreID);
        }
        public Recipe GetRecipeByRecipeID(int RecipeID)
        {
            return this._recipeDAO.GetRecipeByRecipeID(RecipeID);
        }
        public bool EditRecipeDetail(int RecipeDetailID, int Step, int IngreID, float Amount, string Unit, string Desc)
        {
            return this._recipeDetailDAO.EditRecipeDetail(RecipeDetailID, Step, IngreID, Amount, Unit, Desc);
        }
        public bool DeleteRecipeDetail(int RecipeDetailID)
        {
            return this._recipeDetailDAO.DeleteRecipeDetail(RecipeDetailID);
        }
    }
}