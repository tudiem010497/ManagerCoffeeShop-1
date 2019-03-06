using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Context
{
    public class AccountContext:DbContext
    {
        public DbSet<Account> Account { get; set; }
    }
}