using NewTestProject.Interface;
using NewTestProject.Models;
using NewTestProject.Repositories;

namespace NewTestProject;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        var builder = WebApplication.CreateBuilder();
        services.AddControllers();
        services.AddScoped<IDocumentHandler, DocumentHandler>();
        services.AddScoped<ITransferUtilHandler, TransferUtilHandler>();

        services.Configure<AWSConfig>(builder.Configuration.GetSection("AWSCredentials"));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}