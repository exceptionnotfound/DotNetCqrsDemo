using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSLiteDemo.Domain.ReadModel.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        bool EmployeeExists(int employeeID);
        EmployeeRM GetByID(int employeeID);
        IEnumerable<EmployeeRM> GetAll();
    }
}
