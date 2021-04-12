using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Statmath.Application.Task.Data.Context;
using Statmath.Application.Task.DataHelper.Abstraction;
using Statmath.Application.Task.DataHelper.Implementation;
using Statmath.Application.Task.Mapping;

namespace Statmath.Application.Task.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseNpgsql("Server=localhost; Port=5432; Database=application; User Id=postgres; Password=password");
            });

            services.AddSingleton<IDateTimeConverter, DateTimeConverter>();

            services.AddAutoMapper(typeof(MapperProfiles));

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
