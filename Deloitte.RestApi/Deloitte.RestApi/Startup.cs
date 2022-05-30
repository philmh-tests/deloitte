using System.Text.Json;
using Deloitte.RestApi.Database;
using Deloitte.RestApi.Database.Commands;
using Deloitte.RestApi.Database.Commands.Contracts;
using Deloitte.RestApi.Database.Queries;
using Deloitte.RestApi.Database.Queries.Contracts;
using Deloitte.RestApi.Services;
using Deloitte.RestApi.Services.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Deloitte.RestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static string OpenWeatherMapApiKey { get; } = "93a72fbaff67826d6c12278e40762de0";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

            services.AddDbContext<DeloitteContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddMemoryCache();

            // Database Layer
            services.AddScoped<ICityCreateHandler, CityCreateHandler>();
            services.AddScoped<ICityDeleteHandler, CityDeleteHandler>();
            services.AddScoped<ICityModifyHandler, CityModifyHandler>();
            services.AddScoped<ICityQueries, CityQueries>();

            // Service Layer
            services.AddHttpClient<IApiCallerService, ApiCallerService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICountriesService, CountriesService>();
            services.AddScoped<IGeocodingService, GeocodingService>();
            services.AddScoped<IWeatherMapService, WeatherMapService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}