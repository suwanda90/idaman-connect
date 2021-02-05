using ApplicationCore.Helpers;
using ApplicationCore.Interfaces.Auth;
using ApplicationCore.Interfaces.BaseEntity;
using ApplicationCore.Interfaces.Common;
using ApplicationCore.Interfaces.Logging;
using ApplicationCore.Services.Auth;
using Infrastructure.Data;
using Infrastructure.Logging;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Text;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(option =>
            {
                option.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials().Build());
            });

            services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidateAudience = false,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "IP2P Outline Agreement API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); 
            });

            //register service in here
            services.AddApplications();
            services.AddScoped(typeof(IAppLogger<>), typeof(AppLogger<>));
            services.AddTransient<IAuthService, AuthService>();
            services.AddScoped(typeof(IEfRepository<,>), typeof(EfRepository<,>));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IJwt, Jwt>();

            // // this API will accept any access token from the authority
            // services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            //.AddIdentityServerAuthentication(options =>
            //{
            //    options.Authority = "https://login.qa.idaman.pertamina.com/";
            //    options.RequireHttpsMetadata = false;
            //});

            // string[] scopes =
            // {
            //     "file.download"
            // };

            // services.AddAuthorization(options =>
            // {
            //     foreach (var scope in scopes)
            //     {
            //         options.AddPolicy(scope, policy => policy.RequireClaim("scope", "api://1cbac660-6414-4c4c-8dad-7f368edf9851/" + scope));
            //     }
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                c.SwaggerEndpoint("./v1/swagger.json", "IP2P Outline Agreement API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
