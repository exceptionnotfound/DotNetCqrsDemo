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
            //Get the redis database
            var database = _redisConnection.GetDatabase();

            //Get the serialized location
            var locationResult = database.StringGet("location:" + locationID.ToString());

            //If the location doesn't exist
            if (locationResult.IsNull) return null;

            //Otherwise return instance of LocationDTO
            return JsonConvert.DeserializeObject<LocationDTO>(locationResult.ToString());
        }

        public bool LocationExists(int locationID)
        {
            //Get the Redis database
            var database = _redisConnection.GetDatabase();

            //Is there a location with that ID?
            var locationResult = database.StringGet("location:" + locationID.ToString());

            //If there is, return true
            return !locationResult.IsNull;
        }

        public bool HasEmployee(int locationID, int employeeID)
        {
            //Get the Redis database
            var database = _redisConnection.GetDatabase();

            //Deserialize the LocationDTO with the key location:{locationID}
            var location = JsonConvert.DeserializeObject<LocationDTO>(database.StringGet("location:" + locationID.ToString()));

            //If that location has the specified Employee, return true
            return location.Employees.Contains(employeeID);
        }

        public IEnumerable<LocationDTO> GetAll()
        {
            List<LocationDTO> locations = new List<LocationDTO>();

            //Get the Redis database
            var database = _redisConnection.GetDatabase();

            //Get the Redis server
            var server = _redisConnection.GetServer("localhost", 6379);

            //Find all keys on the server which begin with "location:" and end with a number
            var keys = server.Keys(pattern: "location:[0-9]");

            //For each of those keys, deserialize an instance of LocationDTO
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

            //Return a list of EmployeeDTO which represents all Employees at a given Location
            return JsonConvert.DeserializeObject<List<EmployeeDTO>>(database.StringGet("location:" + locationID + ":employees"));
        }
    }
}
