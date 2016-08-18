using CQRSLiteDemo.Domain.ReadModel.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CQRSLiteDemo.Web.Queries.Controllers
{
    [RoutePrefix("employees")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeController(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetByID(int id)
        {
            var employee = _employeeRepo.GetByID(id);
            if(employee == null)
            {
                return BadRequest("No Employee with ID " + id.ToString() + " was found.");
            }
            return Ok(employee);
        }
    }
}
