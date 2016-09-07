using CQRSLiteDemo.Domain.ReadModel.Repositories.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CQRSLiteDemo.Web.Commands.Requests.Employees
{
    public class CreateEmployeeRequest
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string JobTitle { get; set; }
        public int LocationID { get; set; }
    }

    public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
    {
        public CreateEmployeeRequestValidator(IEmployeeRepository employeeRepo, ILocationRepository locationRepo)
        {
            RuleFor(x => x.EmployeeID).Must(x => !employeeRepo.Exists(x)).WithMessage("An Employee with this ID already exists.");
            RuleFor(x => x.LocationID).Must(x => locationRepo.Exists(x)).WithMessage("No Location with this ID exists.");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("The First Name cannot be blank.");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("The Last Name cannot be blank.");
            RuleFor(x => x.JobTitle).NotNull().NotEmpty().WithMessage("The Job Title cannot be blank.");
            RuleFor(x => x.DateOfBirth).LessThan(DateTime.Today.AddYears(-16)).WithMessage("Employees must be 16 years old or older.");
        }
    }
}