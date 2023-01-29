using AutoMapper;
using System;
using System.Collections.Generic;

namespace WFM.GetEngineerTasksServiceLib.Mapper
{
    public class SoAssignmentMapperFactory: ISoAssignmentMapperFactory
    {
        public IMapper CreateSoAssignmentMapper()
        {
            var mapTypes = new (Type Source, Type Target)[]
            {
                (typeof(SsServiceReference.Assignment), typeof(SoServiceReference.Assignment)),
                (typeof(SsServiceReference.AbsenceRequestReference), typeof(SoServiceReference.AbsenceRequestReference)),
                (typeof(SsServiceReference.AggregateStamp), typeof(SoServiceReference.AggregateStamp)),
                (typeof(SsServiceReference.CountryReference), typeof(SoServiceReference.CountryReference)),
                (typeof(SsServiceReference.EngineerReference), typeof(SoServiceReference.EngineerReference)),
                (typeof(SsServiceReference.NonAvailabilityTypeReference), typeof(SoServiceReference.NonAvailabilityTypeReference)),
                (typeof(SsServiceReference.DistrictReference), typeof(SoServiceReference.DistrictReference)),
                (typeof(SsServiceReference.TaskReference), typeof(SoServiceReference.TaskReference))
            };

            var mapper = CreateMapper(mapTypes);

            return mapper;
        }

        private IMapper CreateMapper((Type Source, Type Target)[] ts)
        {
            var config = new MapperConfiguration(cfg => {
                foreach (var t in ts)
                {
                    cfg.CreateMap(t.Source, t.Target);
                }
            });

            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();

            return mapper;
        }
    }
}
