using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class BlogDAOImpl:BlogDAO
    {
        CoffeeShopDBDataContext context;
        public BlogDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public IEnumerable<Blog> GetAllBlog()
        {
            return this.context.Blogs.ToList();
        }
    }
}