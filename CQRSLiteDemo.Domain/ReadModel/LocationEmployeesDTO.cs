using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSLiteDemo.Domain.ReadModel
{
    public class LocationEmployeesDTO
    {
        public List<EmployeeDTO> Employees { get; set; }
    }
}
