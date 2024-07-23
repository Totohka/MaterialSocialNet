using Goods.System.Social.Network.DAL;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Goods.System.Social.Network.DomainServices.Realization;
using Microsoft.EntityFrameworkCore;
using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DAL.Repository.Realization;
using DomainModel.Entities.Dashboard;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Goods.System.Social.Network.Microservice.Users.Mapper;
using NLog.Web;
using NLog;
using FluentValidation;
using DomainServices.Validation;
using Goods.System.Social.Network.Microservice.Users.Middleware;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
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
    builder.Services.AddDbContext<DashboardContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("UserDashboardConnection")).EnableSensitiveDataLogging(), optionsLifetime: ServiceLifetime.Singleton);
    builder.Services.AddDbContextFactory<DashboardContext>();

    builder.Services.AddDbContext<UserContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("UserConnection")).EnableSensitiveDataLogging(), optionsLifetime: ServiceLifetime.Singleton);
    builder.Services.AddDbContextFactory<UserContext>();
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IJWTService, JWTService>();
    builder.Services.AddScoped<IGalleryService, GalleryService>();
    builder.Services.AddScoped<ISubscribeService, SubscribeService>();
    builder.Services.AddScoped<IAvatarService, AvatarService>();

    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<IRepository<CountUsers>, DashboardContextRepository<CountUsers>>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<ISettingPrivacyRepository, SettingPrivacyRepository>();
    builder.Services.AddScoped<ISettingNotificationRepository, SettingNotificationRepository>();
    builder.Services.AddScoped<ISubscribeRepository, SubscribeRepository>();
    builder.Services.AddScoped<IRepository<Message>, Repository<Message>>();
    builder.Services.AddScoped<IRepository<ChatRoom>, Repository<ChatRoom>>();
    builder.Services.AddScoped<IRepository<ChatRoomUser>, Repository<ChatRoomUser>>();
    builder.Services.AddScoped<IRepository<Post>, Repository<Post>>();
    builder.Services.AddScoped<IRepository<UserFriend>, Repository<UserFriend>>();
    builder.Services.AddScoped<IGalleryRepository, GalleryRepository>();
    builder.Services.AddScoped<IPhotoRepository, AvatarRepository>();
    builder.Services.AddScoped<IPhotoRepository, BackgroundRepository>();
    builder.Services.AddScoped<IBackgroundService, Goods.System.Social.Network.DomainServices.Interface.BackgroundService>();

    builder.Services.AddScoped<IValidator<User>, UserValidator>();
    builder.Services.AddScoped<IValidator<int>, IdValidator>();
    builder.Services.AddScoped<IValidator<Post>, PostValidator>();
    builder.Services.AddScoped<IValidator<UserFriend>, UserFriendValidator>();
    builder.Services.AddScoped<IValidator<SettingPrivacy>, SettingPrivacyValidator>();
    builder.Services.AddAutoMapper(typeof(ProfileUser), typeof(ProfileSetting), typeof(ProfileSubscribe));
    builder.Services.AddEndpointsApiExplorer();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    app.UseHttpsRedirection();
    //app.UseRouting();
    app.UseMiddleware<ValidationExceptionHandlingMiddleware>();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseFileServer(new FileServerOptions
    {
        FileProvider = new PhysicalFileProvider(
               Path.Combine(builder.Environment.ContentRootPath, "Galleries")),
        RequestPath = "/Galleries",
        EnableDirectoryBrowsing = true,
    });
    app.UseFileServer(new FileServerOptions
    {
        FileProvider = new PhysicalFileProvider(
               Path.Combine(builder.Environment.ContentRootPath, "Avatars")),
        RequestPath = "/Avatars",
        EnableDirectoryBrowsing = true,
    });
    app.UseFileServer(new FileServerOptions
    {
        FileProvider = new PhysicalFileProvider(
               Path.Combine(builder.Environment.ContentRootPath, "Backgrounds")),
        RequestPath = "/Backgrounds",
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
