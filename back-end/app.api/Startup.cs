using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using app.api.Logging;
using app.api.Security;
using app.Data;
using app.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace app.api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase());

            var connection = Configuration.GetConnectionString("PostgreSqlProvider");
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connection, b => b.MigrationsAssembly("app.api")));

            services.AddAuthentication(options => options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);
            services.AddCors();

            services
            .AddMvc()
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen(c =>
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "app.api.xml");

                c.SwaggerDoc("v1", new Info { Title = "Raz App API", Version = "v1" });
                c.IncludeXmlComments(xmlPath);
            });

            services.AddLogging();
            services.AddOptions();
            services.AddTransient<UserService, UserService>();
            services.AddTransient<TenantService, TenantService>();
            services.AddTransient<PropertyService, PropertyService>();
            services.Configure<Auth0Settings>(Configuration.GetSection("Auth0"));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="auth0Settings"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<Auth0Settings> auth0Settings)
        {
            SetupLogging(app, loggerFactory);
            SetupSwagger(app);
            SetupCookieAuthentication(app);
            SetupOpenID(app, auth0Settings);
            SetupCors(app);

            SetupMvc(app);

            //SetupTestData(app);
        }

        private static void SetupCors(IApplicationBuilder app)
        {
            app.UseCors(builder => builder.AllowAnyOrigin());
        }

        private static void SetupOpenID(IApplicationBuilder app, IOptions<Auth0Settings> auth0Settings)
        {
            var options = new OpenIdConnectOptions("Auth0")
            {
                // Set the authority to your Auth0 domain
                Authority = $"https://{auth0Settings.Value.Domain}",

                // Configure the Auth0 Client ID and Client Secret
                ClientId = auth0Settings.Value.ClientId,
                ClientSecret = auth0Settings.Value.ClientSecret,

                // Do not automatically authenticate and challenge
                AutomaticAuthenticate = false,
                AutomaticChallenge = false,

                // Set response type to code
                ResponseType = "code",

                // Set the callback path, so Auth0 will call back to http://localhost:5000/signin-auth0 
                // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard 
                CallbackPath = new PathString("/signin-auth0"),

                // Configure the Claims Issuer to be Auth0
                ClaimsIssuer = "Auth0",
                Events = new OpenIdConnectEvents
                {
                    // handle the logout redirection 
                    OnRedirectToIdentityProviderForSignOut = (context) =>
                    {
                        var logoutUri = $"https://{auth0Settings.Value.Domain}/v2/logout?client_id={auth0Settings.Value.ClientId}";

                        var postLogoutUri = context.Properties.RedirectUri;
                        if (!string.IsNullOrEmpty(postLogoutUri))
                        {
                            if (postLogoutUri.StartsWith("/"))
                            {
                                // transform to absolute
                                var request = context.Request;
                                postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                            }
                            logoutUri += $"&returnTo={ Uri.EscapeDataString(postLogoutUri)}";
                        }

                        context.Response.Redirect(logoutUri);
                        context.HandleResponse();

                        return Task.CompletedTask;
                    }
                }
            };
            options.Scope.Clear();
            options.Scope.Add("openid");
            options.Scope.Add("name");
            options.Scope.Add("email");
            options.Scope.Add("picture");
            options.Scope.Add("country");
            options.Scope.Add("roles");

            app.UseOpenIdConnectAuthentication(options);
        }

        private static void SetupCookieAuthentication(IApplicationBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });
        }

        private static void SetupTestData(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<AppDbContext>();
            context.Users.Add(new Data.User
            {
                UserID = 1,
                FirstName = "Raz",
                LastName = "Friman",
                Properties = new List<Property>() {
                    new Property {
                        Title = "Villa 1",
                        Tenant = new Tenant {
                            FirstName = "John"
                        }
                    },
                    new Property {
                        Title = "Villa 2"
                    },
                    new Property {
                        Title = "Villa 3"
                    }
                }
            });
            context.Users.Add(new Data.User
            {
                UserID = 2,
                FirstName = "Shlomo",
                LastName = "Friman"
            });

            context.SaveChanges();
        }

        private static void SetupMvc(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default",
                                template: "api/{controller=Default}/{action=Get}/{id?}");
            });
        }

        private static void SetupSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        private void SetupLogging(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddFile("Logs/app-{Date}.txt");
            app.UseErrorLogging();
        }
    }
}
