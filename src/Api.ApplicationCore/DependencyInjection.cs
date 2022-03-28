using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;


namespace Api.ApplicationCore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationCore(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
