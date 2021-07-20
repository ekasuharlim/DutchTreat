
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace DutchTreat.Test
{
    public class CustomDefaultEndpointRouteBuilder : IEndpointRouteBuilder
    {
        public CustomDefaultEndpointRouteBuilder(IApplicationBuilder applicationBuilder)
        {
            ApplicationBuilder = applicationBuilder ?? throw new ArgumentNullException(nameof(applicationBuilder));
            DataSources = new List<EndpointDataSource>();
        }

        public IApplicationBuilder ApplicationBuilder { get; }

        public IApplicationBuilder CreateApplicationBuilder() => ApplicationBuilder.New();

        public ICollection<EndpointDataSource> DataSources { get; }

        public IServiceProvider ServiceProvider => ApplicationBuilder.ApplicationServices;

    }
}
