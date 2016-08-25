using AutoMapper;
using CQRSlite.Commands;
using CQRSLiteDemo.Domain.Commands;
using CQRSLiteDemo.Domain.ReadModel.Repositories.Interfaces;
using CQRSLiteDemo.Web.Commands.Requests.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CQRSLiteDemo.Web.Commands.Controllers
{
    [RoutePrefix("employees")]
    public class EmployeeController : ApiController
    {
        private IMapper _mapper;
        private ICommandSender _commandSender;
        private ILocationRepository _locationRepo;

        public EmployeeController(ICommandSender commandSender, IMapper mapper, ILocationRepository locationRepo)
        {
            _commandSender = commandSender;
            _mapper = mapper;
            _locationRepo = locationRepo;
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(CreateEmployeeRequest request)
        {
            var command = _mapper.Map<CreateEmployeeCommand>(request);
            _commandSender.Send(command);

            var locationAggregateID = _locationRepo.GetByID(request.LocationID).AggregateID;

            var assignCommand = new AssignEmployeeToLocationCommand(locationAggregateID, request.LocationID, request.EmployeeID);
            _commandSender.Send(assignCommand);
            return Ok();
        }
    }
}
