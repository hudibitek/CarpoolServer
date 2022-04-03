using CarPool.DataAccess.Entities;
using CarPool.DataAccess.Faker;
using DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CarPool
{
    public class Startup
    {
        private const string DbConnectionString = "Data Source=carpool.db";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var optionsBuilder = new DbContextOptionsBuilder<CarPoolContext>();
            optionsBuilder.UseSqlite(DbConnectionString);

            var context = new CarPoolContext(optionsBuilder.Options);
            
            // for rebuilding db
            //context.Database.EnsureDeleted();
            //context.Database.Migrate();

            //var faker = new CarPoolFaker();
            //context.Set<Employee>().AddRange(faker.GenerateEmployees());
            //context.Set<Car>().AddRange(faker.GenerateCars());
            //context.SaveChanges();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options
               .AddPolicy("AllowedOriginPolicy", builder =>
                   builder.WithOrigins("https://localhost:4200")
                       .AllowAnyHeader()
                       .WithExposedHeaders("Content-Disposition", "X-Pagination")
                       .AllowAnyMethod()
                       .SetPreflightMaxAge(TimeSpan.FromDays(1))
               )
            );
            services.AddControllers();
            services.AddDbContext<CarPoolContext>(options => options.UseSqlite(DbConnectionString));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfiguration.Init();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowedOriginPolicy");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
