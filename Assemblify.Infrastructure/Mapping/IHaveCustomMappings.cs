using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assemblify.Infrastructure.Mapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappingsForMe(IMapperConfigurationExpression configuration);

        void CreateMappingsForDestination(IMapperConfigurationExpression configuration);
    }
}