using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ManagerCoffeeShopASPNet.Areas.Main.Models
{
    public class LoginModel
    {
        [Required]
        public DateTime DOB { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string IdentityNum { get; set; }
        public string Phone { get; set; }
        public HttpPostedFileBase Avatar { get; set; }
        //public string Avatar { get; set; }
    }
}
