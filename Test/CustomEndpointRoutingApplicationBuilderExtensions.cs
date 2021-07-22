using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing.Constraints;
using DutchTreat.Test;

namespace Microsoft.AspNetCore.Routing
{
    public static class CustomEndpointRoutingApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomEndpoints(this IApplicationBuilder builder, Action<IEndpointRouteBuilder> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            //VerifyRoutingServicesAreRegistered(builder);

            //VerifyEndpointRoutingMiddlewareIsRegistered(builder, out var endpointRouteBuilder);

            var endpointRouteBuilder = new CustomDefaultEndpointRouteBuilder(builder);
            configure(endpointRouteBuilder);

            var routeOptions = builder.ApplicationServices.GetRequiredService<IOptions<RouteOptions>>();
            /*
            foreach (var dataSource in endpointRouteBuilder.DataSources)
            {
                //this can't be done as EndpointDataSources is internal in namespace Microsoft.AspNetCore.Routing
                routeOptions.Value.EndpointDataSources.Add(dataSource);
            }
            */
            return builder.UseMiddleware<CustomEndpointMiddleware>();
        }





    }
}
