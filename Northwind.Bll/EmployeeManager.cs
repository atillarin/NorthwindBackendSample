using Northwind.Bll.Base;
using Northwind.Entity.Dto;
using Northwind.Entity.Models;
using Nortwind.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Bll
{
    public class EmployeeManager : BllBase<Employee, DtoEmployee>, IEmployeeService
    {
        public EmployeeManager(IServiceProvider service):base(service)
        {

        }
    }
}
