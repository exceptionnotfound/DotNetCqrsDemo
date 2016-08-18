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
    }
}
