using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSLiteDemo.Domain.ReadModel.Repositories.Interfaces
{
    public interface ILocationRepository : IBaseRepository<LocationRM>
    {
        IEnumerable<LocationRM> GetAll();
        IEnumerable<EmployeeRM> GetEmployees(int locationID);
        bool HasEmployee(int locationID, int employeeID);
    }
}
