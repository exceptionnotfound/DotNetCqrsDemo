using AutoMapper;
using CQRSlite.Commands;
using CQRSLiteDemo.Domain.Commands;
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

        public LocationController(ICommandSender commandSender, IMapper mapper)
        {
            _commandSender = commandSender;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(CreateLocationRequest request)
        {
            var command = _mapper.Map<CreateLocationCommand>(request);
            _commandSender.Send(command);
            return Ok();
        }
    }
}
