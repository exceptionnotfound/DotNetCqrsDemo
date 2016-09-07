using AutoMapper;
using CQRSlite.Events;
using CQRSLiteDemo.Domain.Events.Employees;
using CQRSLiteDemo.Domain.ReadModel;
using CQRSLiteDemo.Domain.ReadModel.Repositories.Interfaces;
using CQRSLiteDemo.Domain.WriteModel;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSLiteDemo.Domain.EventHandlers
{
    public class EmployeeEventHandler : IEventHandler<EmployeeCreatedEvent>
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepo;
        public EmployeeEventHandler(IConnectionMultiplexer redis, IMapper mapper, IEmployeeRepository employeeRepo)
        {
            _redis = redis;
            _mapper = mapper;
            _employeeRepo = employeeRepo;
        }

        public void Handle(EmployeeCreatedEvent message)
        {
            EmployeeRM employee = _mapper.Map<EmployeeRM>(message);
            _employeeRepo.Save(employee);
        }
    }
}
