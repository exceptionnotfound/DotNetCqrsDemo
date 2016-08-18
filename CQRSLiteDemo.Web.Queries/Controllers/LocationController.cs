using CQRSLiteDemo.Domain.ReadModel.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CQRSLiteDemo.Web.Queries.Controllers
{
    [RoutePrefix("location")]
    public class LocationController : ApiController
    {
        private ILocationRepository _locationRepo;

        public LocationController(ILocationRepository locationRepo)
        {
            _locationRepo = locationRepo;
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetByID(int id)
        {
            var location = _locationRepo.GetByID(id);
            if(location == null)
            {
                return BadRequest("No location with ID " + id.ToString() + " was found.");
            }
            return Ok(location);
        }
    }
}
