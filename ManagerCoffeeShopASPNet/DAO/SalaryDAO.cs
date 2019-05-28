﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface SalaryDAO
    {
        Salary GetSalaryByDesc(string Desc);
        Salary GetSalaryBySalaryID(int SalaryID);
    }
}
