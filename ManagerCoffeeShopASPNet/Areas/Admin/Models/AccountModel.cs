using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Areas.Admin.Models
{
    [JsonObject]
    public class AccountModel
    {
        [JsonProperty(PropertyName = "EmployeeID")]
        public int EmployeeID { get; set; }
        [JsonProperty(PropertyName = "UserName")]
        public string UserName { get; set; }
        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }
        [JsonProperty(PropertyName = "AccType")]
        public string AccType { get; set; }
        [JsonProperty(PropertyName = "Position")]
        public string Position { get; set; }
        [JsonProperty(PropertyName = "Avatar")]
        public string Avatar { get; set; }
        public HttpPostedFileBase AvatarUpload { get; set; }
    }
}