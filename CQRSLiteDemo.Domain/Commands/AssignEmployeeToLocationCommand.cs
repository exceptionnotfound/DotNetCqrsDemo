using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSLiteDemo.Domain.Commands
{
public class AssignEmployeeToLocationCommand : BaseCommand
{
    public readonly int EmployeeID;
    public readonly int LocationID;

    public AssignEmployeeToLocationCommand(int locationID, int employeeID)
    {
        EmployeeID = employeeID;
        LocationID = locationID;
    }
}
}
