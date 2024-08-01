using Goods.System.Social.Network.DAL;
using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DAL.Repository.Realization;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Goods.System.Social.Network.DomainServices.Realization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NLog.Web;
using NLog;
using Goods.System.Social.Network.Microservice.Posts.Infrastructure.Mapper;
using DomainServices.Validation;
using FluentValidation;
using Goods.System.Social.Network.Microservice.Reaction.Infrastructure.Middleware;
using Goods.System.Social.Network.Microservice.Posts.Services.Interface;
using Goods.System.Social.Network.Microservice.Posts.Services.Implementation;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try { 
    var builder = WebApplication.CreateBuilder(args);
    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.WebHost.UseNLog();

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
    // Add services to the container.
    builder.Services.AddDbContext<UserContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("UserConnection")).EnableSensitiveDataLogging(), optionsLifetime: ServiceLifetime.Singleton);
    builder.Services.AddDbContextFactory<UserContext>();

    builder.Services.AddScoped<IPostService, PostService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IReactionEntityService<ReactionPost>, ReactionPostService<ReactionPost>>();

    builder.Services.AddScoped<IReactionEntityRepository<ReactionPost>, ReactionEntityRepository<ReactionPost>>();
    builder.Services.AddScoped<IReactionRepository, ReactionRepository>();
    builder.Services.AddScoped<IPostRepository, PostRepository>();
    builder.Services.AddScoped<IImagePostRepository, ImagePostRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<ISettingPrivacyRepository, SettingPrivacyRepository>();
    builder.Services.AddScoped<ISettingNotificationRepository, SettingNotificationRepository>();
    builder.Services.AddScoped<ISubscribeRepository, SubscribeRepository>();
    builder.Services.AddScoped<INotificationPostProducer, NotificationPostProducer>();

    builder.Services.AddAutoMapper(typeof(ProfilePost));

    builder.Services.AddScoped<IValidator<User>, UserValidator>();
    builder.Services.AddScoped<IValidator<SettingPrivacy>, SettingPrivacyValidator>();
    builder.Services.AddScoped<IValidator<ReactionData>, ReactionValidator>();
    builder.Services.AddScoped<IValidator<Post>, PostValidator>();
    builder.Services.AddScoped<IValidator<int>, IdValidator>();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        var basePath = AppContext.BaseDirectory;

        var xmlPath = Path.Combine(basePath, "Goods.System.Social.Network.Microservice.Posts.xml");
        options.IncludeXmlComments(xmlPath);
    });

    builder.Services.AddDirectoryBrowser();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment())
    //{
        app.UseSwagger();
        app.UseSwaggerUI();
    //}

    app.UseHttpsRedirection();
    app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    app.UseMiddleware<ValidationExceptionHandlingMiddleware>();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseFileServer(new FileServerOptions
    {
        FileProvider = new PhysicalFileProvider(
               Path.Combine(builder.Environment.ContentRootPath, "PostsData")),
        RequestPath = "/PostsData",
        EnableDirectoryBrowsing = true,
    });
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