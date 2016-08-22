using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSLiteDemo.Domain.Events.Locations
{
    public class EmployeeRemovedFromLocationEvent : BaseEvent
    {
        public readonly int OldLocationID;
        public readonly int EmployeeID;

        public EmployeeRemovedFromLocationEvent(Guid id, int oldLocationID, int employeeID)
        {
            Id = id;
            OldLocationID = oldLocationID;
            EmployeeID = employeeID;
        }
    }
}
