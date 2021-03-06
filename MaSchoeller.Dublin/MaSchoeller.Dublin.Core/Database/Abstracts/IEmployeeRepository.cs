﻿using MaSchoeller.Dublin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Database.Abstracts
{
    public interface IEmployeeRepository : IRepository<Employee>
    {

        IEnumerable<Employee> GetPossibleEmployeesByVehicle(int id);
    }
}
