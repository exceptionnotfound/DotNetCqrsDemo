using AutoMapper;
using CQRSlite.Commands;
using CQRSLiteDemo.Domain.Commands;
using CQRSLiteDemo.Domain.ReadModel.Repositories.Interfaces;
using CQRSLiteDemo.Web.Commands.Requests.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CQRSLiteDemo.Web.Commands.Controllers
{
    [RoutePrefix("locations")]
    public class LocationController : ApiController
    {
        private IMapper _mapper;
        private ICommandSender _commandSender;
        private ILocationRepository _locationRepo;
        private IEmployeeRepository _employeeRepo;

        public LocationController(ICommandSender commandSender, IMapper mapper, ILocationRepository locationRepo, IEmployeeRepository employeeRepo)
        {
            _commandSender = commandSender;
            _mapper = mapper;
            _locationRepo = locationRepo;
            _employeeRepo = employeeRepo;
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(CreateLocationRequest request)
        {
            var command = _mapper.Map<CreateLocationCommand>(request);
            _commandSender.Send(command);
            return Ok();
        }

        [HttpPost]
        [Route("assignemployee")]
        public IHttpActionResult AssignEmployee(AssignEmployeeToLocationRequest request)
        {
            var employee = _employeeRepo.GetByID(request.EmployeeID);
            if (employee.LocationID != 0)
            {
                var oldLocationAggregateID = _locationRepo.GetByID(employee.LocationID).AggregateID;

                RemoveEmployeeFromLocationCommand removeCommand = new RemoveEmployeeFromLocationCommand(oldLocationAggregateID, request.LocationID, employee.EmployeeID);
                _commandSender.Send(removeCommand);
            }

            var locationAggregateID = _locationRepo.GetByID(request.LocationID).AggregateID;
            var assignCommand = new AssignEmployeeToLocationCommand(locationAggregateID, request.LocationID, request.EmployeeID);
            _commandSender.Send(assignCommand);

            return Ok();
        }
    }
}
