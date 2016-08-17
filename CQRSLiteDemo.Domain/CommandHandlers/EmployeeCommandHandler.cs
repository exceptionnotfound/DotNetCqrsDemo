using CQRSlite.Commands;
using CQRSlite.Domain;
using CQRSLiteDemo.Domain.Commands;
using CQRSLiteDemo.Domain.WriteModel.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSLiteDemo.Domain.CommandHandlers
{
    public class EmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand>
    {
        private readonly ISession _session;

        public EmployeeCommandHandler(ISession session)
        {
            _session = session;
        }

        public void Handle(CreateEmployeeCommand command)
        {
            Employee employee = new Employee(command.Id, command.FirstName, command.LastName, command.DateOfBirth, command.JobTitle);
            _session.Add(employee);
            _session.Commit();
        }
    }
}
