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
    public class LocationRepository : ILocationRepository
    {
        private readonly IConnectionMultiplexer _redisConnection;

        public LocationRepository(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
        }
        public LocationDTO GetByID(int locationID)
        {
            var database = _redisConnection.GetDatabase();
            var locationResult = database.StringGet("location:" + locationID.ToString());
            if (locationResult.IsNull) return null;
            return JsonConvert.DeserializeObject<LocationDTO>(locationResult.ToString());
        }

        public bool LocationExists(int locationID)
        {
            var database = _redisConnection.GetDatabase();
            var locationResult = database.StringGet("location:" + locationID.ToString());
            return !locationResult.IsNull;
        }

        public bool HasEmployee(int locationID, int employeeID)
        {
            var database = _redisConnection.GetDatabase();
            var location = JsonConvert.DeserializeObject<LocationDTO>(database.StringGet("location:" + locationID.ToString()));
            return location.Employees.Contains(employeeID);
        }

        public IEnumerable<LocationDTO> GetAll()
        {
            List<LocationDTO> locations = new List<LocationDTO>();
            var database = _redisConnection.GetDatabase();
            var server = _redisConnection.GetServer("localhost", 6379);
            var keys = server.Keys(pattern: "location:*");
            foreach (var key in keys)
            {
                locations.Add(JsonConvert.DeserializeObject<LocationDTO>(database.StringGet(key)));
            }
            return locations;
        }
        public IEnumerable<EmployeeDTO> GetEmployees(int locationID)
        {
            List<EmployeeDTO> employees = new List<EmployeeDTO>();
            var database = _redisConnection.GetDatabase();
            return JsonConvert.DeserializeObject<List<EmployeeDTO>>(database.StringGet("location:" + locationID + ":employees"));
        }
    }
}
