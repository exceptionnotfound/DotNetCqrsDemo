// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CQRSLiteDemo.Web.Commands.DependencyResolution {
    using AutoMapper;
    using CQRSlite.Bus;
    using CQRSlite.Cache;
    using CQRSlite.Commands;
    using CQRSlite.Domain;
    using CQRSlite.Events;
    using Domain.Events;
    using Domain.EventStore;
    using Requests.Employees;
    using StackExchange.Redis;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
    using StructureMap.Web;
    using System;
    using System.Linq;

    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {
            //Commands, Events, Handlers
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.AssemblyContainingType<BaseEvent>();
                    scan.Convention<FirstInterfaceConvention>();
                });

            //CQRSLite
            For<InProcessBus>().Singleton().Use<InProcessBus>();
            For<ICommandSender>().Use(y => y.GetInstance<InProcessBus>());
            For<IEventPublisher>().Use(y => y.GetInstance<InProcessBus>());
            For<IHandlerRegistrar>().Use(y => y.GetInstance<InProcessBus>());
            For<ISession>().HybridHttpOrThreadLocalScoped().Use<Session>();
            For<IEventStore>().Singleton().Use<InMemoryEventStore>();
            For<IRepository>().HybridHttpOrThreadLocalScoped().Use(y =>
                    new CacheRepository(new Repository(y.GetInstance<IEventStore>()), y.GetInstance<IEventStore>()));

            //AutoMapper
            var profiles = from t in typeof(DefaultRegistry).Assembly.GetTypes()
                           where typeof(Profile).IsAssignableFrom(t)
                           select (Profile)Activator.CreateInstance(t);

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            var mapper = config.CreateMapper();

            For<IMapper>().Use(mapper);

            //StackExchange.Redis
            ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect("localhost");

            For<IConnectionMultiplexer>().Use(multiplexer);

            //FluentValidation
            FluentValidation.AssemblyScanner.FindValidatorsInAssemblyContaining<CreateEmployeeRequestValidator>()
              .ForEach(result =>
              {
                  For(result.InterfaceType)
                     .Use(result.ValidatorType);
              });
        }

        #endregion
    }
}