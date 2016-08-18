using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSLiteDemo.Domain.Commands
{
    public class CreateLocationCommand : BaseCommand
    {
        public readonly string StreetAddress;
        public readonly string City;
        public readonly string State;
        public readonly string PostalCode;

        public CreateLocationCommand(Guid id, string streetAddress, string city, string state, string postalCode)
        {
            Id = id;
            StreetAddress = streetAddress;
            City = city;
            State = state;
            PostalCode = postalCode;
        }
    }
}
