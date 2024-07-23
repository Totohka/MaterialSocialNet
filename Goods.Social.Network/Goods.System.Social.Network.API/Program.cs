using Goods.System.Social.Network.DAL.Repository;
using Goods.System.Social.Network.DAL;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Goods.System.Social.Network.DomainServices.Realization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Goods.System.Social.Network.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<UserContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("UserConnection")).EnableSensitiveDataLogging(), optionsLifetime: ServiceLifetime.Singleton);
builder.Services.AddDbContextFactory<UserContext>();
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IChatService, ChatService>();

//builder.Services.AddMvcCore().AddAuthorization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepository<User>, Repository<User>>();
builder.Services.AddScoped<IRepository<Role>, Repository<Role>>();
builder.Services.AddScoped<IRepository<ChatRoom>, Repository<ChatRoom>>();
builder.Services.AddScoped<IRepository<ChatRoomUser>, Repository<ChatRoomUser>>();
builder.Services.AddScoped<IRepository<Message>, Repository<Message>>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
    builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                // указывает, будет ли валидироваться издатель при валидации токена
                ValidateIssuer = true,
                // строка, представляющая издателя
                ValidIssuer = AuthOptions.ISSUER,

                // будет ли валидироваться потребитель токена
                ValidateAudience = true,
                // установка потребителя токена
                ValidAudience = AuthOptions.AUDIENCE,
                // будет ли валидироваться время существования
                ValidateLifetime = true,

                // установка ключа безопасности
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                // валидация ключа безопасности
                ValidateIssuerSigningKey = true,
            };
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];

                    // если запрос направлен хабу
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) &&
                        (path.StartsWithSegments("/chat")))
                    {
                        // получаем токен из строки запроса
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
        });

builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chat");
});
app.MapControllers();
app.Run(async (context) =>
{
    app.Logger.LogInformation($"Path: {context.Request.Path}  Time:{DateTime.Now.ToLongTimeString()}");
    await context.Response.WriteAsync("Hello World!");
});
app.Run();
