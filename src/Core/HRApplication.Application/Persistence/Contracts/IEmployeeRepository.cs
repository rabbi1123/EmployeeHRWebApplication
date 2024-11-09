using HRApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApplication.Application.Persistence.Contracts
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
    }
}
