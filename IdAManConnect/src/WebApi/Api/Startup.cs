using ApplicationCore.Helpers;
using ApplicationCore.Interfaces.BaseEntity;
using ApplicationCore.Interfaces.Logging;
using IdentityServer4.AccessTokenValidation;
using Infrastructure.Data;
using Infrastructure.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "IdamanConnect API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            //register service in here
            services.AddApplications();
            services.AddScoped(typeof(IAppLogger<>), typeof(AppLogger<>));
            services.AddScoped(typeof(IEfRepository<,>), typeof(EfRepository<,>));

            services.AddControllers();
            services.AddCors();
            services.AddDistributedMemoryCache();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
           .AddIdentityServerAuthentication(options =>
           {
               options.Authority = Configuration["Idaman:UrlLogin"];
               options.RequireHttpsMetadata = true;
           });

            var scopes = Configuration["Idaman:Scopes"].Trim().Replace(" ", "").Split(",");

            services.AddAuthorization(options =>
            {
                options.AddPolicy("api.auth", policy => policy.RequireClaim("scope", "api.auth"));
                foreach (var scope in scopes)
                {
                    options.AddPolicy(scope, policy => policy.RequireClaim("scope", "api://" + Configuration["Idaman:ObjectId"] + "/" + scope));
                }
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dataContext, IAppLogger<ApplicationDbContext> appLogger)
        {
            dataContext.Database.Migrate();

            try
            {
                ApplicationDbContextSeed.SeedAsync(dataContext, appLogger).Wait();
            }
            catch (Exception ex)
            {
                appLogger.LogError(ex.Message.ToString());
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./v1/swagger.json", "IdamanConnect API");
            });

            app.UseHttpsRedirection();

            app.UseCors(policy =>
            {
                policy.WithOrigins(
                    "https://localhost:44337");

                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
            });
        }
    }
}
