using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Statmath.Application.Data.Context;
using Statmath.Application.DataHelper.Abstraction;
using Statmath.Application.DataHelper.Implementation;
using Statmath.Application.Mapping;
using Statmath.Application.Repository.Abstraction;
using Statmath.Application.Repository.Implementation;

namespace Statmath.Application.Api
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
            var connectionString = Configuration["ConnectionString"];
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<ApplicationDbContext>(opt => 
                    opt.UseNpgsql(connectionString, 
                        b => b.MigrationsAssembly("Statmath.Application.Api")
                    )
                );

            services.AddScoped<IDateTimeConverter, DateTimeConverter>();
            services.AddScoped<IDateTimeHelper, DateTimeHelper>();
            services.AddScoped<IJobRepository, JobRepository>();

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