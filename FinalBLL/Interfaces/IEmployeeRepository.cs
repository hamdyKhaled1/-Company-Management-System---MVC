using FinalDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBLL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        IQueryable<Employee> SearchEmployee(string name);
       
    }
}
