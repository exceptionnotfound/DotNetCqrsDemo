using AutoMapper;
using CQRSLiteDemo.Domain.Commands;
using CQRSLiteDemo.Web.Commands.Requests.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CQRSLiteDemo.Web.Commands.AutoMapperConfig
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<CreateLocationRequest, CreateLocationCommand>()
                .ConstructUsing(x => new CreateLocationCommand(Guid.NewGuid(), x.StreetAddress, x.City, x.State, x.PostalCode));
        }
    }
}