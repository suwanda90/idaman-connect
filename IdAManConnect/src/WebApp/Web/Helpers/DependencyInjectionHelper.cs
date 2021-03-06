﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Web.Interfaces;
using Web.Services;

namespace Web.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static IServiceCollection AddApplications(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IIdamanService, IdamanService>();
            
            return services;
        }
    }
}

