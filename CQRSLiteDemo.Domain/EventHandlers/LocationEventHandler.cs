using AutoMapper;
using CQRSlite.Events;
using CQRSLiteDemo.Domain.Events.Locations;
using CQRSLiteDemo.Domain.ReadModel;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSLiteDemo.Domain.EventHandlers
{
    public class LocationEventHandler : IEventHandler<LocationCreatedEvent>
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IMapper _mapper;
        public LocationEventHandler(IConnectionMultiplexer redis, IMapper mapper)
        {
            _redis = redis;
            _mapper = mapper;
        }

        public void Handle(LocationCreatedEvent message)
        {
            var database = _redis.GetDatabase();
            LocationDTO location = _mapper.Map<LocationDTO>(message);
            database.StringSet("location:" + message.LocationID, JsonConvert.SerializeObject(location));
        }
    }
}
