using CQRSLiteDemo.Domain.ReadModel.Repositories.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSLiteDemo.Domain.ReadModel.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConnectionMultiplexer _redisConnection;

        public EmployeeRepository(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
        }

        public bool EmployeeExists(int employeeID)
        {
            var database = _redisConnection.GetDatabase();
            var employee = database.StringGet("employee:" + employeeID.ToString());
            return !employee.IsNull;
        }

        public EmployeeDTO GetByID(int employeeID)
        {
            //Get the Redis database
            var database = _redisConnection.GetDatabase();

            //Get the value for the "employee:{employeeID}" key
            var employee = database.StringGet("employee:" + employeeID.ToString());

            //If we don't find anything
            if (employee.IsNull) return null;

            //Otherwise, return the EmployeeDTO
            return JsonConvert.DeserializeObject<EmployeeDTO>(employee.ToString());
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
            List<EmployeeDTO> employees = new List<EmployeeDTO>();
            var database = _redisConnection.GetDatabase();
            var server = _redisConnection.GetServer("localhost", 6379);
            var keys = server.Keys(pattern: "employee:*");
            foreach(var key in keys)
            {
                employees.Add(JsonConvert.DeserializeObject<EmployeeDTO>(database.StringGet(key)));
            }
            return employees;
        }
    }
}
