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
public class LocationRepository : BaseRepository, ILocationRepository
{
    public LocationRepository(IConnectionMultiplexer redisConnection) : base(redisConnection, "location") { }
    public LocationRM GetByID(int locationID)
    {
        return Get<LocationRM>(locationID);
    }

    public List<LocationRM> GetMultiple(List<int> locationIDs)
    {
        return GetMultiple(locationIDs);
    }

    public bool HasEmployee(int locationID, int employeeID)
    {
        //Deserialize the LocationDTO with the key location:{locationID}
        var location = Get<LocationRM>(locationID);

        //If that location has the specified Employee, return true
        return location.Employees.Contains(employeeID);
    }

    public IEnumerable<LocationRM> GetAll()
    {
        return Get<List<LocationRM>>("all");
    }
    public IEnumerable<EmployeeRM> GetEmployees(int locationID)
    {
        return Get<List<EmployeeRM>>(locationID.ToString() + ":employees");
    }

    public void Save(LocationRM location)
    {
        Save(location.LocationID, location);
        MergeIntoAllCollection(location);
    }

    private void MergeIntoAllCollection(LocationRM location)
    {
        List<LocationRM> allLocations = new List<LocationRM>();
        if (Exists("all"))
        {
            allLocations = Get<List<LocationRM>>("all");
        }

        //If the district already exists in the ALL collection, remove that entry
        if (allLocations.Any(x => x.LocationID == location.LocationID))
        {
            allLocations.Remove(allLocations.First(x => x.LocationID == location.LocationID));
        }

        //Add the modified district to the ALL collection
        allLocations.Add(location);

        Save("all", allLocations);
    }
}
}
