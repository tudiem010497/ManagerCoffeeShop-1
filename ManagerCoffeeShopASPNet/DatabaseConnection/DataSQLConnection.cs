using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DatabaseConnection
{
    public class DataSQLConnection
    {
        private string sqlConnection = "Data Source=DESKTOP-HA2TCUF;Initial Catalog=CoffeeShopDB;Integrated Security=True";
        private SqlConnection con { get; set; }
        public void Conncet()
        {

            if (con == null)

                con = new SqlConnection(sqlConnection);

            if (con.State == ConnectionState.Closed)

                con.Open();
        }
        public void Disconnect()
        {
            if ((con != null) && (con.State == ConnectionState.Open))
            {
                con.Close();
            }
        }
    }
}