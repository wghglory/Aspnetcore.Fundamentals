using Aspnetcore.Fundamentals.Models;
using Aspnetcore.Fundamentals.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aspnetcore.Fundamentals
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(Configuration);
            services.AddSingleton<IGreeter, Greeter>();
            //            services.AddScoped<IRestaurantData, InMemoryRestaurantData>();
            services.AddScoped<IRestaurantData, MySqlRestaurantData>();

            /* AddSingleton: a single instance of a service that is used throughout the application, everyone sees the same instance. 
			 * AddScoped: add a service instance that will be scoped to an HTTP request. 
			 * So all of the components inside of a single request will see the same instance, 
			 * but across two different HTTP requests, there will be two different instances. */

            services.AddDbContext<FoodDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("MysqlConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<FoodDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IGreeter greeter
        )
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // stage, production add error handler
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = (context) => context.Response.WriteAsync("Opps!")
                });
            }

            app.UseFileServer(); //Microsoft.AspNetCore.StaticFiles

            app.UseNodeModules(env.ContentRootPath); // middleware to read node_modules as static files
            
            app.UseIdentity();

            // app.UseMvcWithDefaultRoute();
            app.UseMvc(ConfigureRoutes);


            //app.Run(async (context) =>
            //{
            //    string message = greeter.GetGreeting();
            //    await context.Response.WriteAsync(message);
            //}); 
        }

        private static void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}