using CQRSlite.Events;
using CQRSLiteDemo.Domain.Events.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSLiteDemo.Domain.EventHandlers
{
    public class EmployeeEventHandler : IEventHandler<EmployeeCreatedEvent>
    {
        public void Handle(EmployeeCreatedEvent message)
        {

        }
    }
}
