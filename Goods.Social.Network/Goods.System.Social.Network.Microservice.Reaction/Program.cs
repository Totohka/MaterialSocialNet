using Goods.System.Social.Network.DAL;
using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DAL.Repository.Realization;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using NLog;
using System.Text;
using Goods.System.Social.Network.Microservice.Reaction.Infrastructure.Mapper;
using FluentValidation;
using DomainServices.Validation;
using Goods.System.Social.Network.Microservice.Reaction.Infrastructure.Middleware;
using DomainServices.Comments.Interface;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
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

    builder.Services.AddDbContext<DashboardContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("UserDashboardConnection")).EnableSensitiveDataLogging(), optionsLifetime: ServiceLifetime.Singleton);
    builder.Services.AddDbContextFactory<DashboardContext>();

    builder.Services.AddDbContext<UserContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("UserConnection")).EnableSensitiveDataLogging(), optionsLifetime: ServiceLifetime.Singleton);
    builder.Services.AddDbContextFactory<UserContext>();

    builder.Services.AddControllers();

    builder.Services.AddScoped<ITypeReactionService, TypeReactionService>();
    builder.Services.AddScoped<IReactionEntityService<ReactionPost>, ReactionPostService<ReactionPost>>();
    builder.Services.AddScoped<IReactionEntityService<ReactionMessage>, ReactionMessageService<ReactionMessage>>();

    builder.Services.AddScoped<ITypeReactionRepository, TypeReactionRepository>();
    builder.Services.AddScoped<IReactionRepository, ReactionRepository>();
    builder.Services.AddScoped<IReactionEntityRepository<ReactionPost>, ReactionEntityRepository<ReactionPost>>();
    builder.Services.AddScoped<IReactionEntityRepository<ReactionMessage>, ReactionEntityRepository<ReactionMessage>>();

    builder.Services.AddScoped<IValidator<ReactionType>, ReactionTypeValidator>();
    builder.Services.AddScoped<IValidator<ReactionData>, ReactionValidator>();
    builder.Services.AddScoped<IValidator<int>, IdValidator>();

    builder.Services.AddAutoMapper(typeof(ProfileReaction));

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        var basePath = AppContext.BaseDirectory;

        var xmlPath = Path.Combine(basePath, "Goods.System.Social.Network.Microservice.Reaction.xml");
        options.IncludeXmlComments(xmlPath);
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment())
    //{
        app.UseSwagger();
        app.UseSwaggerUI();
    //}

    app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    app.UseMiddleware<ValidationExceptionHandlingMiddleware>();
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped PostMicroservice because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}
