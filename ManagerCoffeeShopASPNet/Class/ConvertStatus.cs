using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Class
{
    public class ConvertStatusOrder
    {
        public string ConvertDesc(string Desc)
        {
            if (Desc == "ServeAtCafe")
                return "Dùng tại quán";
            else if (Desc == "TakeAway")
                return "Mang về";
            else if (Desc == "Delivery")
                return "Giao hàng";
            else return "";
        }
        public string ConvertStatus(string Status)
        {
            if (Status == "WaitToConfirm")
            {
                return "Chờ xác nhận";
            }
            else if (Status == "Pending")
            {
                return "Đang pha chế";
            }
            else if (Status == "Cancel")
            {
                return "Hủy";
            }
            else if (Status == "Ready")
            {
                return "Đã pha chế xong";
            }
            else if (Status == "Closed")
            {
                return "Đã phục vụ";
            }
            else if (Status == "Paid")
            {
                return "Đã thanh toán";
            }
            else return "";
        }
    }
    public class ConvertStatusTest{
        public string test(string Status)
        {
            // Status bảng Position
            if (Status == "Not available")
            {
                Status = "Trống";
            }
            else if (Status == "Available")
            {
                Status = "Có người";
            }
            else if (Status == "Reserved")
            {
                Status = "Đã đặt";
            }
            else if (Status == "Close")
            {
                Status = "Đã đến";
            }
            else if (Status == "Not yet delivered")
            {
                Status = "Chưa giao";
            }
            else if (Status == "Failed")
            {
                Status = "Giao không thành công";
            }
            //Status bảng Employee
            else if (Status == "Fulltime")
            {
                Status = "Chính thức";
            }
            else if (Status == "Parttime")
            {
                Status = "Bán thời gian";
            }
            else if (Status == "Lay_off")
            {
                Status = "Nghỉ việc";
            }
            else if (Status == "Manager")
            {
                Status = "Quản lý";
            }
            //Status bảng IngredientMessage
            else if (Status == "WaitingConfirm")
            {
                Status = "Chờ xác nhận";
            }
            else if (Status == "Accept")
            {
                Status = "Đồng ý";
            }
            else if (Status == "NotAccept")
            {
                Status = "Không đồng ý";
            }
            //Status bảng Receipt (giống order)
            else if (Status == "Not yet imported")
            {
                Status = "Chưa nhập";
            }
            else if (Status == "Inputting")
            {
                Status = "Đang nhập";
            }
            else if (Status == "Entered")
            {
                Status = "Đã nhập";
            }
            else if (Status == "Paid")
            {
                Status = "Đã thanh toán";
            }
            return Status;
        }
    }
}