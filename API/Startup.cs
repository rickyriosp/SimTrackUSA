using API.Extensions;
using API.Helpers;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API;

public class Startup
{
    private readonly IConfiguration _config;

    public Startup(IConfiguration config)
    {
        _config = config;
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(_config.GetConnectionString("DefaultConnection"),
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

        services.AddSingleton<IConnectionMultiplexer>(provider =>
        {
            var configuration = ConfigurationOptions.Parse(_config.GetConnectionString("Redis"), true);
            return ConnectionMultiplexer.Connect(configuration);
        });

        services.AddApplicationServices();

        services.AddAutoMapper(typeof(MappingProfiles));
        services.AddHttpContextAccessor();

        services.AddSwaggerDocumentation();

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                policy => { policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"); });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // app.UseMiddleware<ExceptionMiddleware>();
        app.UseExceptionMiddleware();

        if (env.IsDevelopment())
        {
            //app.UseDeveloperExceptionPage();
        }

        app.UseSwaggerDocumentation();

        app.UseStatusCodePagesWithReExecute("/errors/{0}");

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseStaticFiles();

        app.UseCors("CorsPolicy");

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
