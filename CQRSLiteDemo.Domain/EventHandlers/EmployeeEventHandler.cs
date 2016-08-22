using AutoMapper;
using CQRSlite.Events;
using CQRSLiteDemo.Domain.Events.Employees;
using CQRSLiteDemo.Domain.ReadModel;
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
    public EmployeeEventHandler(IConnectionMultiplexer redis, IMapper mapper)
    {
        _redis = redis;
        _mapper = mapper;
    }

    public void Handle(EmployeeCreatedEvent message)
    {
        var database = _redis.GetDatabase();
        EmployeeDTO employee = _mapper.Map<EmployeeDTO>(message);
        database.StringSet("employee:" + message.EmployeeID.ToString(), JsonConvert.SerializeObject(employee));
    }
}
}
