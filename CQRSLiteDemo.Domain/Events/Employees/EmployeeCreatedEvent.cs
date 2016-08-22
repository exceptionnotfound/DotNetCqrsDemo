using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSLiteDemo.Domain.Events.Employees
{
public class EmployeeCreatedEvent : BaseEvent
{
    public readonly int EmployeeID;
    public readonly string FirstName;
    public readonly string LastName;
    public readonly DateTime DateOfBirth;
    public readonly string JobTitle;

    public EmployeeCreatedEvent(Guid id, int employeeID, string firstName, string lastName, DateTime dateOfBirth, string jobTitle)
    {
        Id = id;
        EmployeeID = employeeID;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        JobTitle = jobTitle;
    }
}
}
