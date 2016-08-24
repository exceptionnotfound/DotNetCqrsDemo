using AutoMapper;
using CQRSlite.Commands;
using CQRSLiteDemo.Domain.Commands;
using CQRSLiteDemo.Web.Commands.Requests.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CQRSLiteDemo.Web.Commands.Controllers
{
    [RoutePrefix("employee")]
    public class EmployeeController : ApiController
    {
        private IMapper _mapper;
        private ICommandSender _commandSender;

        public EmployeeController(ICommandSender commandSender, IMapper mapper)
        {
            _commandSender = commandSender;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(CreateEmployeeRequest request)
        {
            var command = _mapper.Map<CreateEmployeeCommand>(request);
            _commandSender.Send(command);

            var assignCommand = new AssignEmployeeToLocationCommand(request.LocationID, request.EmployeeID);
            _commandSender.Send(assignCommand);
            return Ok();
        }
    }
}
