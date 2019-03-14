using ManagerCoffeeShopASPNet.DAO;
using ManagerCoffeeShopASPNet.DAOImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Information
{
    public class InformationService
    {
        private FoodAndDrinkDAO _foodAndDrinkDAO;
        private OrderDAO _orderDAO;
        private OrderItemDAO _orderItemDAO;
        private PositionDAO _positionDAO;
        public InformationService()
        {
            this._foodAndDrinkDAO = (FoodAndDrinkDAO)new FoodAndDrinkDAOImpl();
            this._orderDAO = (OrderDAO)new OrderDAOImpl();
            this._orderItemDAO = (OrderItemDAO)new OrderItemDAOImpl();
            this._positionDAO = (PositionDAO)new PositionDAOImpl();
        }
        public IEnumerable<FoodAndDrink> GetFoodAndDrink()
        {
            return _foodAndDrinkDAO.GetAllFoodAndDrink();
        }
        public FoodAndDrink GetFoodAndDrinkByID(int id)
        {
            return _foodAndDrinkDAO.GetFoodAndDrinkByID(id);
        }
        public void InsertOrder(int PosID, DateTime OrderDateTime,
            float TotalAmount, string Currency, string Desc, string Status)
        {
            _orderDAO.InsertOrder(PosID, OrderDateTime,
           TotalAmount, Currency, Desc, Status);
        }
        public void InsertOrderItem(int OrderID, int FDID, int Quantity, string Desc, string Status)
        {
            _orderItemDAO.InsertOrderItem(OrderID, FDID, Quantity, Desc, Status);
        }
        public IEnumerable<Position> GetAllPosition()
        {
            return _positionDAO.GetAllPosition();
        }
    }
}