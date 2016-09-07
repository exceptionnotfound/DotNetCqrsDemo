using AutoMapper;
using CQRSlite.Events;
using CQRSLiteDemo.Domain.Events.Locations;
using CQRSLiteDemo.Domain.ReadModel;
using CQRSLiteDemo.Domain.ReadModel.Repositories.Interfaces;
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
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepo;
        private readonly IEmployeeRepository _employeeRepo;
        public LocationEventHandler(IMapper mapper, ILocationRepository locationRepo, IEmployeeRepository employeeRepo)
        {
            _mapper = mapper;
            _locationRepo = locationRepo;
            _employeeRepo = employeeRepo;
        }

        public void Handle(LocationCreatedEvent message)
        {
            //Create a new LocationDTO object from the LocationCreatedEvent
            LocationRM location = _mapper.Map<LocationRM>(message);

            _locationRepo.Save(location);
        }

        public void Handle(EmployeeAssignedToLocationEvent message)
        {
            var location = _locationRepo.GetByID(message.NewLocationID);
            location.Employees.Add(message.EmployeeID);
            _locationRepo.Save(location);

            //Find the employee which was assigned to this Location
            var employee = _employeeRepo.GetByID(message.EmployeeID);
            employee.LocationID = message.NewLocationID;
            _employeeRepo.Save(employee);
        }

        public void Handle(EmployeeRemovedFromLocationEvent message)
        {
            var location = _locationRepo.GetByID(message.OldLocationID);
            location.Employees.Remove(message.EmployeeID);
            _locationRepo.Save(location);
        }
    }
}
