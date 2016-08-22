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
    public class LocationEventHandler : IEventHandler<LocationCreatedEvent>,
                                        IEventHandler<EmployeeAssignedToLocationEvent>,
                                        IEventHandler<EmployeeRemovedFromLocationEvent>
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
        //Get the Redis instance we will be interfacing with
        var database = _redis.GetDatabase();

        //Create a new LocationDTO object from the LocationCreatedEvent
        LocationDTO location = _mapper.Map<LocationDTO>(message);

        //Serialize the new LocationDTO into the Redis store, with key "location:{LocationID}"
        database.StringSet("location:" + message.LocationID, JsonConvert.SerializeObject(location));
    }

        public void Handle(EmployeeAssignedToLocationEvent message)
        {
            //Get the Redis instance we will be interfacing with
            var database = _redis.GetDatabase();

            //Deserialize the LocationDTO object specified by the "location:{locationID}" key
            var location = JsonConvert.DeserializeObject<LocationDTO>(database.StringGet("location:" + message.NewLocationID.ToString()));

            //Add the specified employee to the LocationDTO object
            location.Employees.Add(message.EmployeeID);

            //Re-serialize the changed LocationDTO
            database.StringSet("location:" + message.NewLocationID, JsonConvert.SerializeObject(location));

            //Find the employee which was assigned to this Location
            var employee = JsonConvert.DeserializeObject<EmployeeDTO>(database.StringGet("employee:" + message.EmployeeID.ToString()));

            //Set the employee's LocationID property
            employee.LocationID = message.NewLocationID;

            //Re-serialize the employee back into the datastore.
            database.StringSet("employee:" + message.EmployeeID, JsonConvert.SerializeObject(employee));
        }

        public void Handle(EmployeeRemovedFromLocationEvent message)
        {
            //Get the Redis instance
            var database = _redis.GetDatabase();

            //Deserialize the LocationDTO object with the key "location:{locationID}"
            var location = JsonConvert.DeserializeObject<LocationDTO>(database.StringGet("location:" + message.OldLocationID.ToString()));

            //Remove the employee from the location
            location.Employees.Remove(message.EmployeeID);

            //Serialize the changed LocationDTO back into the data store.
            database.StringSet("location:" + message.OldLocationID, JsonConvert.SerializeObject(location));
        }
    }
}
