using DomainModel.Entities.Dashboard;
using Goods.System.Social.Network.DAL;
using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DAL.Repository.Realization;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Goods.System.Social.Network.DomainServices.Realization;
using Microsoft.EntityFrameworkCore;
using System;
using NLog.Web;
using NLog;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try { 
    var builder = WebApplication.CreateBuilder(args);
    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.WebHost.UseNLog();
    // Add services to the container.
    builder.Services.AddDbContext<DashboardContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("UserDashboardConnection")).EnableSensitiveDataLogging(), optionsLifetime: ServiceLifetime.Singleton);
    builder.Services.AddDbContextFactory<DashboardContext>();

    builder.Services.AddDbContext<UserContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("UserConnection")).EnableSensitiveDataLogging(), optionsLifetime: ServiceLifetime.Singleton);
    builder.Services.AddDbContextFactory<UserContext>();

    builder.Services.AddControllers();
    builder.Services.AddScoped<IDashboardService, DashboardService>();
    builder.Services.AddScoped<IRepository<CountUsers>, DashboardContextRepository<CountUsers>>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();
    app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped DashboardMicroservice because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}