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
            var database = _redisConnection.GetDatabase();
            var employee = database.StringGet("employee:" + employeeID.ToString());
            if (employee.IsNull) return null;
            return JsonConvert.DeserializeObject<EmployeeDTO>(employee.ToString());
        }
    }
}
