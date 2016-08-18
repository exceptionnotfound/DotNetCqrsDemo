using CQRSlite.Domain;
using CQRSLiteDemo.Domain.Events.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSLiteDemo.Domain.WriteModel
{
    public class Location : AggregateRoot
    {
        private int _locationID;
        private string _streetAddress;
        private string _city;
        private string _state;
        private string _postalCode;
        private List<Guid> _employees;

        private Location() { }

        public Location(Guid id, int locationID, string streetAddress, string city, string state, string postalCode)
        {
            Id = id;
            _locationID = locationID;
            _streetAddress = streetAddress;
            _city = city;
            _state = state;
            _postalCode = postalCode;
            _employees = new List<Guid>();

            ApplyChange(new LocationCreatedEvent(id, locationID, streetAddress, city, state, postalCode));
        }
    }
}
