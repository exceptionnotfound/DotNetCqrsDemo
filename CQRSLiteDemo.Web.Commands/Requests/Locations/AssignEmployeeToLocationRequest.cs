using CQRSLiteDemo.Domain.ReadModel.Repositories.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CQRSLiteDemo.Web.Commands.Requests.Locations
{
    public class AssignEmployeeToLocationRequest
    {
        public int LocationID { get; set; }
        public int EmployeeID { get; set; }
    }

    public class AssignEmployeeToLocationRequestValidator : AbstractValidator<AssignEmployeeToLocationRequest>
    {
        public AssignEmployeeToLocationRequestValidator(IEmployeeRepository employeeRepo, ILocationRepository locationRepo)
        {
            RuleFor(x => x.LocationID).Must(x => locationRepo.Exists(x)).WithMessage("No Location with this ID exists.");
            RuleFor(x => x.EmployeeID).Must(x => employeeRepo.Exists(x)).WithMessage("No Employee with this ID exists.");
            RuleFor(x => new { x.LocationID, x.EmployeeID }).Must(x => !locationRepo.HasEmployee(x.LocationID, x.EmployeeID)).WithMessage("This Employee is already assigned to that Location.");
        }
    }
}