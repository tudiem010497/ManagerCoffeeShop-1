using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class AccountDAOImpl : AccountDAO
    {
        private CoffeeShopDBDataContext context;
        public AccountDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public IEnumerable<Account> GetAllAccount()
        {
            return context.Accounts.ToList();
        }
        public int GetLastUserID()
        {
            int id = (from user in context.Accounts orderby user.UserID descending select user.UserID).FirstOrDefault();
            return id;
        }
        public Account GetAccountByEmail(string Email)
        {
            Account acc = (from account in context.Accounts
                           where account.Email == Email
                           select account).SingleOrDefault();
            return acc;
        }
        public Account GetAccountByUserName(string UserName)
        {
            Account acc = (from account in context.Accounts
                           where account.UserName == UserName
                           select account).SingleOrDefault();
            return acc;
        }
        public Account GetAccountByUserID(int UserID)
        {
            Account acc = (from account in context.Accounts
                           where account.UserID == UserID
                           select account).SingleOrDefault();
            return acc;
        }
        public IEnumerable<Account> GetAccountByID(int UserID)
        {
            IEnumerable<Account> acc = (from account in context.Accounts
                                        where account.UserID == UserID
                                        select account);
            return acc;
        }
        public bool InsertAccount(string UserName, string Password, string Email, string AccType, string Position, string Avatar)
        {
            try
            {
                int UserID = GetLastUserID() + 1;
                Account acc = new Account();
                acc.UserName = UserName;
                acc.Password = Password;
                acc.Email = Email;
                acc.AccType = AccType;
                acc.Position = Position;
                acc.Avatar = Avatar;
                context.Accounts.InsertOnSubmit(acc);
                context.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error insert account " + e.Message);
            }
        }
        public bool DeleteAccount(int UserID)
        {
            try
            {
                Account acc = GetAccountByUserID(UserID);
                context.Accounts.DeleteOnSubmit(acc);
                context.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error delete account " + e.Message);
            }
        }
    }
}