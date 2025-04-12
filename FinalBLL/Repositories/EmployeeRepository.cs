using FinalBLL.Interfaces;
using FinalDAL.Context;
using FinalDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MVCAppContext _dbContext;// هعمل دي لما يكون عندي ميثود مخصوص بترجع حاجه معينه من الموظفين
        public EmployeeRepository(MVCAppContext dbContxt) : base(dbContxt)
        {
            _dbContext = dbContxt; 
        }

        public IQueryable<Employee> SearchEmployee(string name)
        {
           return _dbContext.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower()));
        }
    }
}
