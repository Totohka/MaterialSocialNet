using Goods.System.Social.Network.DAL;
using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DAL.Repository.Realization;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Goods.System.Social.Network.DomainServices.Realization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using System;
using System.Data;
using System.Text;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try { 
    var builder = WebApplication.CreateBuilder(args);
    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.WebHost.UseNLog();
    // Add services to the container.
    builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));
    var secretKey = builder.Configuration.GetSection("JWTSettings:SecretKey").Value;
    var issuer = builder.Configuration.GetSection("JWTSettings:Issuer").Value;
    var audience = builder.Configuration.GetSection("JWTSettings:Audience").Value;
    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            IssuerSigningKey = signingKey,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
    });
    builder.Services.AddDbContext<UserContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("UserConnection")).EnableSensitiveDataLogging(), optionsLifetime: ServiceLifetime.Singleton);
    builder.Services.AddDbContextFactory<UserContext>();
    builder.Services.AddScoped<IJWTService, JWTService>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IPhotoRepository, AvatarRepository>();
    builder.Services.AddControllers();
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
    app.UseDeveloperExceptionPage();
    app.UseHttpsRedirection();
    app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
 }
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}