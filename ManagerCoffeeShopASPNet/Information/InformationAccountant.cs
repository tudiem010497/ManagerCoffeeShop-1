using ManagerCoffeeShopASPNet.DAO;
using ManagerCoffeeShopASPNet.DAOImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Information
{
    public class InformationAccountant
    {
        private OrderDAO _orderDAO;
        private OrderItemDAO _orderItemDAO;
        private FoodAndDrinkDAO _foodAndDrinkDAO;
        private ReceiptDAO _receiptDAO;
        private ReceiptDetailDAO _receiptDetailDAO;
        private EmployeeDAO _employeeDAO;
        private TimeSheetDAO _timeSheetDAO;
        private TimeSheetDetailDAO _timeSheetDetailDAO;
        private PayrollDAO _payrollDAO;
        private AccountDAO _accDAO;
        public InformationAccountant()
        {
            this._orderDAO = (OrderDAO)new OrderDAOImpl();
            this._orderItemDAO = (OrderItemDAO)new OrderItemDAOImpl();
            this._receiptDAO = (ReceiptDAO)new ReceiptDAOImpl();
            this._receiptDetailDAO = (ReceiptDetailDAO)new ReceiptDetailDAOImpl();
            this._timeSheetDAO = (TimeSheetDAO)new TimeSheetDAOImpl();
            this._timeSheetDetailDAO = (TimeSheetDetailDAO)new TimeSheetDetailDAOImpl();
            this._payrollDAO = (PayrollDAO)new PayrollDAOImpl();
            this._accDAO = (AccountDAO)new AccountDAOImpl();
            this._employeeDAO = (EmployeeDAO)new EmployeeDAOImpl();
        }
        public Employee GetInfoEmployeeByEmID(int EmID)
        {
            return this._employeeDAO.GetEmployeeByID(EmID);
        }
        public IEnumerable<Payroll> GetAllAddedOnOfPayrollByEmID(int EmID)
        {
            return this._payrollDAO.GetAllAddedOnOfPayrollByEmID(EmID);
        }
        public IEnumerable<Employee> GetAllEmployee()
        {
            return this._employeeDAO.GetAllEmployee();
        }
        public Account GetAccountByEmail(string Email)
        {
            return this._accDAO.GetAccountByEmail(Email);
        } 

        /// <summary>
        /// Tính tổng hóa đơn (chưa tính khuyến mãi)
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public double CalculateTotalAmount(int OrderID)
        {
            IEnumerable<OrderItem> orderItems = this._orderItemDAO.GetAllOrderItemByOrderID(OrderID);
            double totalAmount = 0;
            foreach (OrderItem item in orderItems)
            {
                if (item.Status == "Closed")
                {
                    totalAmount = totalAmount + item.Quantity * this._foodAndDrinkDAO.GetFoodAndDrinkByID(item.FDID).UnitPrice;
                }
            }
            return totalAmount;
        }

        public IEnumerable<Receipt> GetAllReceipt()
        {
            return this._receiptDAO.GetAllReceipt();
        }
        public IEnumerable<Receipt> GetReceiptWaitToConfirm()
        {
            return this._receiptDAO.GetReceiptWaitToConfirm();
        }
        public IEnumerable<Receipt> GetReceiptByReceiptID(int ReceiptID)
        {
            return this._receiptDAO.GetReceiptByReceiptID(ReceiptID);
        }
        public IEnumerable<ReceiptDetail> GetReceiptDetailByReceiptID(int ReceiptID)
        {
            return this._receiptDetailDAO.GetReceiptDetailByReceiptID(ReceiptID);
        }
        public ReceiptDetail GetReceiptDetailByReceiptDetailID(int ReceiptDetailID)
        {
            return this._receiptDetailDAO.GetReceiptDetailByReceiptDetailID(ReceiptDetailID);
        }
        public bool UpdateReceipt(int ReceiptID, string Status)
        {
            return this._receiptDAO.UpdateReceipt(ReceiptID, Status);
        }
        public bool UpdateReceiptDetail(int ReceiptDetailID, string Status)
        {
            return this._receiptDetailDAO.UpdateReceiptDetail(ReceiptDetailID, Status);
        }
        public TimeSheet GetTimeSheetByEmployeeID(int EmployeeID)
        {
            return this._timeSheetDAO.GetTimeSheetByEmployeeID(EmployeeID);
        }
        public bool InsertTimeSheet(int EmployeeID, int WorkDay, int Total, string Currency)
        {
            return this._timeSheetDAO.InsertTimeSheet(EmployeeID, WorkDay, Total, Currency);
        }
        public bool InsertTimeSheetDetail(int TimeSheetID, int Bonus, int Penalty, string Currency, string Desc)
        {
            return this._timeSheetDetailDAO.InsertTimeSheetDetail(TimeSheetID, Bonus, Penalty, Currency, Desc);
        }
        public Payroll GetPayrollByEmployeeID(int EmployeeID)
        {
            return this._payrollDAO.GetPayrollByEmployeeID(EmployeeID);
        }
        public Payroll GetPayrollByEmployeeIDAndAddedOnNo(int EmployeeID, DateTime AddedOn)
        {
            return this._payrollDAO.GetPayrollByEmployeeIDAndAddedOnNo(EmployeeID, AddedOn);
        }
        public IEnumerable<Payroll> GetAllAddedOnOfPayroll()
        {
            return this._payrollDAO.GetAllAddedOnOfPayroll();
        }
        public bool InsertPayroll(int EmployeeID, string EmployeeName, int BasicSalary, int WorkDay, int Bonus, int Penalty, int Total, string Currency, string Desc, DateTime AddedOn)
        {
            return this._payrollDAO.InsertPayroll(EmployeeID, EmployeeName, BasicSalary, WorkDay, Bonus, Penalty, Total, Currency, Desc, AddedOn);
        }
        public IEnumerable<Payroll> GetParyollByEmployeeIDAndAddedOn(int EmployeeName, DateTime AddedOn)
        {
            return this._payrollDAO.GetParyollByEmployeeIDAndAddedOn(EmployeeName, AddedOn);
        }
        public bool EditPayroll(int PayrollID, int WorkDay, int Bonus, int Penalty, int Total, string Currency, string Desc)
        {
            return this._payrollDAO.EditPayroll(PayrollID, WorkDay, Bonus, Penalty, Total, Currency, Desc);
        }
        public Payroll GetPayrollByPayrollID(int PayrollID)
        {
            return this._payrollDAO.GetPayrollByPayrollID(PayrollID);
        }
        public bool CheckTimeSheetOfEmployee(int EmployeeID)
        {
            return this._timeSheetDAO.CheckTimeSheetOfEmployee(EmployeeID);
        }
    }
}