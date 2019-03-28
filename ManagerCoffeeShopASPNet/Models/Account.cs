using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Models
{
    [Table("Account")]
    public class Account // test 28/03/2019
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        private int UserID { get; set; }
        private string UserName { get; set; }
        private string Password { get; set; }
        private string AccType { get; set; }
        private string Position { get; set; }
        private string Avatar { get; set; }
    }
}