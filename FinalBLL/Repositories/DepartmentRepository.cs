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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly MVCAppContext _dbContext;
        public DepartmentRepository(MVCAppContext dbContext):base(dbContext)
        {
            _dbContext= dbContext;
        }

        public IQueryable<Department> SearchDepartment(string name)
        {
          return  _dbContext.Departments.Where(D => D.Name.ToLower().Contains(name.ToLower()));
        }
    }
}
