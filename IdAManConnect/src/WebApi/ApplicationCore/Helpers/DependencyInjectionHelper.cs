using ApplicationCore.Interfaces.Config;
using ApplicationCore.Services.Config;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationCore.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static IServiceCollection AddApplications(this IServiceCollection services)
        {
            //config
            services.AddTransient<IRoleService, RoleService>();

            return services;
        }
    }
}

