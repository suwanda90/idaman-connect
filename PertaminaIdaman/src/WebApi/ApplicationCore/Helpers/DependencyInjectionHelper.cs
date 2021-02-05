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
            services.AddTransient<IClientApiService, ClientApiService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IMenuRoleService, MenuRoleService>();

            return services;
        }
    }
}

