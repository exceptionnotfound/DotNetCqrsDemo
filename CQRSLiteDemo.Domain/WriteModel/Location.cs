using CQRSlite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSLiteDemo.Domain.WriteModel
{
    public class Location : AggregateRoot
    {
        private string _streetAddress;
        private string _city;
        private string _state;
        private string _postalCode;
        private List<Guid> _employees;

        private Location() { }

        public Location(Guid id, string streetAddress, string city, string state, string postalCode)
        {
            Id = id;
            _streetAddress = streetAddress;
            _city = city;
            _state = state;
            _postalCode = postalCode;
            _employees = new List<Guid>();

            //TODO: ApplyChange
        }
    }
}
