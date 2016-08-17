using CQRSlite.Domain;
using CQRSLiteDemo.Domain.Events.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSLiteDemo.Domain.WriteModel.Employees
{
    public class Employee : AggregateRoot
    {
        private string _firstName;
        private string _lastName;
        private DateTime _dateOfBirth;
        private string _jobTitle;

        private Employee() { }

        public Employee(Guid id, string firstName, string lastName, DateTime dateOfBirth, string jobTitle)
        {
            Id = id;
            _firstName = firstName;
            _lastName = lastName;
            _dateOfBirth = dateOfBirth;
            _jobTitle = jobTitle;

            ApplyChange(new EmployeeCreatedEvent(id, firstName, lastName, dateOfBirth, jobTitle));
        }
    }
}
