using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Areas.Admin.Models
{
    [JsonObject]
    public class PayrollModel
    {
        [JsonProperty(PropertyName ="EmployeeID")]
        public int EmployeeID { get; set; }
        [JsonProperty(PropertyName ="EmployeeName")]
        public string EmployeeName { get; set; }
        [JsonProperty(PropertyName ="Position")]//chức vụ
        public string Position { get; set; }
        [JsonProperty(PropertyName ="WorkDay")]//ngày công
        public int WorkDay { get; set; }
        [JsonProperty(PropertyName = "SalaryType")]
        public int SalaryType { get; set; }
        [JsonProperty(PropertyName ="BasicSalary")]//lương căn bản dựa trên mã lương
        public int BasicSalary { get; set; }
        [JsonProperty(PropertyName ="Bonus")]//phụ cấp tiền thưởng
        public int Bonus { get; set; }
        [JsonProperty(PropertyName = "Penalty")]//phụ cấp tiền phạt
        public int Penalty { get; set; }
        [JsonProperty(PropertyName ="Desc")]//ghi chú
        public string Desc { get; set; }
        [JsonProperty(PropertyName = "Total")]
        public int Total { get; set; }
    }
}