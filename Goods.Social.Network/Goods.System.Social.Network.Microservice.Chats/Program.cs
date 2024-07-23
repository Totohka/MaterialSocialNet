using Goods.System.Social.Network.DAL;
using Goods.System.Social.Network.DomainModel.Entities;
using Microsoft.EntityFrameworkCore;
using Goods.System.Social.Network.Microservice.Chats;
using Goods.System.Social.Network.DAL.Repository.Realization;
using Goods.System.Social.Network.DAL.Repository.Interface;
using DomainServices.Chat.Realization;
using DomainServices.Chat.Interface;
using NLog.Web;
using NLog;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Goods.System.Social.Network.Microservice.Chats.Infrastructure.Mapper;
using DomainServices.Validation;
using Goods.System.Social.Network.Microservice.Reaction.Infrastructure.Middleware;
using FluentValidation;
using Goods.System.Social.Network.DomainServices.Interface;
using Goods.System.Social.Network.Microservice.Chats.Services.Interface;
using Goods.System.Social.Network.Microservice.Chats.Services.Implementation;
using DomainServices.Comments.Interface;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);
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
        options.Events = new JwtBearerEvents()
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Path.ToString().StartsWith("/chat"))
                    context.Token = context.Request.Query["t"];
                return Task.CompletedTask;
            },
        };
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
    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.WebHost.UseNLog();

    // Add services to the container.

    builder.Services.AddDbContext<UserContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("UserConnection")).EnableSensitiveDataLogging(), optionsLifetime: ServiceLifetime.Singleton);
    builder.Services.AddDbContextFactory<UserContext>();
    builder.Services.AddControllers();
    builder.Services.AddScoped<ITypeReactionService, TypeReactionService>();
    builder.Services.AddScoped<IChatService, ChatService>();
    builder.Services.AddScoped<IInviteService, InviteService>();
    builder.Services.AddScoped<IMessageService, MessageService>();
    builder.Services.AddScoped<IReactionEntityService<ReactionMessage>, ReactionMessageService<ReactionMessage>>();
    builder.Services.AddScoped<INotificationChatRoomProducer, NotificationChatRoomProducer>();
    
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.EnableAnnotations();
    });
    //builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "Chatslogger.txt"));
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IChatRepository, ChatRepository>();
    builder.Services.AddScoped<IRepository<ChatRoomUser>, Repository<ChatRoomUser>>();
    builder.Services.AddScoped<IMessageRepository, MessageRepository>();
    builder.Services.AddScoped<ITypeReactionRepository, TypeReactionRepository>();
    builder.Services.AddScoped<IReactionRepository, ReactionRepository>();
    builder.Services.AddScoped<IReactionEntityRepository<ReactionMessage>, ReactionEntityRepository<ReactionMessage>>();


    builder.Services.AddAutoMapper(typeof(ProfileChat));

    builder.Services.AddScoped<IValidator<ReactionType>, ReactionTypeValidator>();
    builder.Services.AddScoped<IValidator<Message>, MessageValidator>();
    builder.Services.AddScoped<IValidator<ChatRoom>, ChatRoomValidator>();
    builder.Services.AddScoped<IValidator<ChatRoomUser>, ChatRoomUserValidator>();
    builder.Services.AddScoped<IValidator<int>, IdValidator>();
    builder.Services.AddScoped<IValidator<ReactionData>, ReactionValidator>();
    builder.Services.AddSignalR(options =>
    {
        options.EnableDetailedErrors = true;
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseRouting();
    app.UseCors(x => x
       .AllowAnyMethod()
       .AllowAnyHeader()
       .AllowAnyOrigin());

    app.UseMiddleware<ValidationExceptionHandlingMiddleware>();
    app.UseAuthorization();
    app.MapHub<ChatHub>("/chat");
    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped ChatMicroservice because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}