using ManagerCoffeeShopASPNet.DAO;
using ManagerCoffeeShopASPNet.DAOImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Information
{
    public class InformationIndex
    {
        private FoodAndDrinkDAO _foodAndDrinkDAO;
        private MenuDAO _menuDAO;
        private BannerImageDAO _bannerImageDAO;
        private InfoIndexDAO _infoIndexDAO;
        private BlogDAO _blogDAO;
        private AccountDAO _accountDAO;
        private CustomerDAO _customerDAO;

        public InformationIndex()
        {
            this._foodAndDrinkDAO = (FoodAndDrinkDAO)new FoodAndDrinkDAOImpl();
            this._menuDAO = (MenuDAO)new MenuDAOImpl();
            this._bannerImageDAO = (BannerImageDAO)new BannerImageDAOImpl();
            this._infoIndexDAO = (InfoIndexDAO)new InfoIndexDAOImpl();
            this._accountDAO = (AccountDAO)new AccountDAOImpl();
            this._customerDAO = (CustomerDAO)new CustomerDAOImpl();
        }
        public IEnumerable<FoodAndDrink> GetFoodAndDrink()
        {
            //IEnumerable<Account> accounts = accDAO.GetAllAccount();
            //return accounts
            IEnumerable<FoodAndDrink> fds = _foodAndDrinkDAO.GetAllFoodAndDrink();
            return fds;
        }
        public IEnumerable<Menu> GetMenu()
        {
            return _menuDAO.getAllMenu();
        }
        public IEnumerable<BannerImage> GetBannerImage()
        {
            return _bannerImageDAO.GetAllBannerImage();
        }
        public IEnumerable<InfoIndex> GetInfoIndex()
        {
            return _infoIndexDAO.GetInfoIndex();
        }
        //public IEnumerable<Blog> GetBlog()
        //{
        //    return _blogDAO.GetAllBlog();
        //}
        public Account GetAccountByEmail(string Email)
        {
            return _accountDAO.GetAccountByEmail(Email);
        }
        public Account GetAccountByUserID(int UserID)
        {
            return _accountDAO.GetAccountByUserID(UserID);
        }
        public bool InsertCustomer(int UserID, string Name, DateTime DOB, string Address, string IdentityNum, string Phone)
        {
            return _customerDAO.InsertCustomer(UserID, Name, DOB, Address, IdentityNum, Phone);
        }
        
    }
}