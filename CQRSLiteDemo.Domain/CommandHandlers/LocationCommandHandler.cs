using CQRSlite.Commands;
using CQRSlite.Domain;
using CQRSLiteDemo.Domain.Commands;
using CQRSLiteDemo.Domain.WriteModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSLiteDemo.Domain.CommandHandlers
{
    public class LocationCommandHandler : ICommandHandler<CreateLocationCommand>
    {
        private readonly ISession _session;

        public LocationCommandHandler(ISession session)
        {
            _session = session;
        }

        public void Handle(CreateLocationCommand command)
        {
            var location = new Location(command.Id, command.StreetAddress, command.City, command.State, command.PostalCode);
            _session.Add(location);
            _session.Commit();
        }
    }
}
