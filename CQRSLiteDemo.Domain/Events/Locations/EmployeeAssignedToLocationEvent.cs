using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSLiteDemo.Domain.Events.Locations
{
    public class EmployeeAssignedToLocationEvent : BaseEvent
    {
        public readonly int NewLocationID;
        public readonly int EmployeeID;

        public EmployeeAssignedToLocationEvent(Guid id, int newLocationID, int employeeID)
        {
            Id = id;
            NewLocationID = newLocationID;
            EmployeeID = employeeID;
        }
    }
}
