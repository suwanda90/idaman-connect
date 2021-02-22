using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Web.Helpers;
using Web.ViewModels;

namespace Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Action<AppSettingsViewModel> appSettings = (opt =>
            {
                opt.ApiUrl = Configuration["Api:Url"];
                opt.ApplicationCookiesName = Configuration["Application:CookiesName"];
                opt.ApplicationFolderApp = Configuration["Application:FolderApp"];
                opt.IdamanUrlLogin = Configuration["Idaman:UrlLogin"];
                opt.IdamanUrlApi = Configuration["Idaman:UrlApi"];
                opt.IdamanClientId = Configuration["Idaman:ClientId"];
                opt.IdamanClientSecret = Configuration["Idaman:ClientSecret"];
                opt.IdamanScopes = Configuration["Idaman:Scopes"];
                opt.IdamanConnectApiObjectId = Configuration["IdamanConnectApi:ObjectId"];
                opt.IdamanConnectApiClientId = Configuration["IdamanConnectApi:ClientId"];
                opt.IdamanConnectApiClientSecret = Configuration["IdamanConnectApi:ClientSecret"];
                opt.IdamanConnectApiScopes = Configuration["IdamanConnectApi:Scopes"];
            });

            services.Configure(appSettings);
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<AppSettingsViewModel>>().Value);

            services.AddControllersWithViews()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                        options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    });

            //register service in here
            services.AddApplications();
            services.AddMemoryCache();

            //idaman
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.SignInScheme = "Cookies";
                    options.Authority = Configuration["Idaman:UrlLogin"];
                    options.RequireHttpsMetadata = false;

                    options.ClientId = Configuration["Idaman:ClientId"];
                    options.ClientSecret = Configuration["Idaman:ClientSecret"];
                    options.ResponseType = "code id_token";

                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;

                    var scopes = Configuration["Idaman:Scopes"].Replace(" ", "").Split(",");
                    foreach (var scope in scopes)
                    {
                        options.Scope.Add(scope);
                    }

                    options.Events = new OpenIdConnectEvents
                    {
                        // Your events here
                        OnRemoteFailure = ctx =>
                        {
                            ctx.HandleResponse();
                            ctx.Response.Redirect("/");
                            return Task.FromResult(0);
                        }
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppSettingsViewModel appSettingsViewModel)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            //if use sub folder/sub domain
            if (!string.IsNullOrEmpty(appSettingsViewModel.ApplicationFolderApp))
            {
                app.UsePathBase(appSettingsViewModel.ApplicationFolderApp);
            }

            app.UseRouting();

            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
