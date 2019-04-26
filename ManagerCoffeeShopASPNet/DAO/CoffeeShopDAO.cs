using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface CoffeeShopDAO
    {
        IEnumerable<CoffeeShop> GetAllCoffeeShop();
        bool InsertCoffeeShop(string Name, string Address, string Phone, string LogoImagePath, string TitleAbout, string DescAbout, string TitleContact, string DescContact, string Email);

    }
}