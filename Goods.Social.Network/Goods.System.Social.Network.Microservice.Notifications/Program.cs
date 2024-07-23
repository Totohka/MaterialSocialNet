using DAL.Repository.Notifications.Implementation;
using DAL.Repository.Notifications.Interface;
using DomainServices.Chat.Interface;
using DomainServices.Chat.Realization;
using DomainServices.Notifications.Implementation;
using DomainServices.Validation;
using FluentValidation;
using Goods.System.Social.Network.DAL;
using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DAL.Repository.Realization;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Goods.System.Social.Network.DomainServices.Realization;
using Goods.System.Social.Network.Microservice.Notifications;
using Goods.System.Social.Network.Microservice.Posts.Services.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

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
    options.Events = new JwtBearerEvents()
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Path.ToString().StartsWith("/notifications"))
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
builder.Services.AddDbContext<UserContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("UserConnection")).EnableSensitiveDataLogging(), optionsLifetime: ServiceLifetime.Singleton);
builder.Services.AddDbContextFactory<UserContext>();
builder.Services.AddControllers();
builder.Services.AddSingleton<INotificationService, NotificationService>();
builder.Services.AddSingleton<IChatService, ChatService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<INotificationPostService, NotificationPostService>();
builder.Services.AddSingleton<INotificationChatRoomService, NotificationChatRoomService>();

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ISettingPrivacyRepository, SettingPrivacyRepository>();
builder.Services.AddSingleton<ISettingNotificationRepository, SettingNotificationRepository>();
builder.Services.AddSingleton<ISubscribeRepository, SubscribeRepository>();
builder.Services.AddSingleton<IChatRepository, ChatRepository>();
builder.Services.AddSingleton<IRepository<ChatRoomUser>, Repository<ChatRoomUser>>();
builder.Services.AddSingleton<IMessageRepository, MessageRepository>();
builder.Services.AddSingleton<INotificationRepository, NotificationRepository>();
builder.Services.AddSingleton<INotificationPostRepository, NotificationPostRepository>();
builder.Services.AddSingleton<INotificationChatRoomRepository, NotificationChatRoomRepository>();

builder.Services.AddSingleton<IValidator<User>, UserValidator>();
builder.Services.AddSingleton<IValidator<int>, IdValidator>();
builder.Services.AddSingleton<IValidator<Post>, PostValidator>();
builder.Services.AddSingleton<IValidator<UserFriend>, UserFriendValidator>();
builder.Services.AddSingleton<IValidator<SettingPrivacy>, SettingPrivacyValidator>();
builder.Services.AddSingleton<IValidator<Message>, MessageValidator>();
builder.Services.AddSingleton<IValidator<ChatRoom>, ChatRoomValidator>();
builder.Services.AddSingleton<IValidator<ChatRoomUser>, ChatRoomUserValidator>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<NotificationConsumer>();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());

app.UseAuthentication();
app.UseAuthorization();
app.MapHub<NotificationHub>("/notifications");
app.MapControllers();

app.Run();
