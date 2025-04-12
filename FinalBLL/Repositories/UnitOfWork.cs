using FinalBLL.Interfaces;
using FinalDAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MVCAppContext _dbContext;
        public IEmployeeRepository EmployeeRepository { get ; set; }
        public IDepartmentRepository DepartmentRepository { get ; set ; }
        public UnitOfWork(MVCAppContext dbContext)
        {
            EmployeeRepository=new EmployeeRepository(dbContext);
            DepartmentRepository=new DepartmentRepository(dbContext);
            _dbContext = dbContext;
        }
        public async Task<int> Complete()
        => await _dbContext.SaveChangesAsync();

        public void Dispose()
        =>_dbContext.Dispose();
    }
}
