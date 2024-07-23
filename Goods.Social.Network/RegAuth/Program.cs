using Goods.Social.Network.DAL;
using Goods.Social.Network.DomainModel;
using Microsoft.EntityFrameworkCore;
using Goods.Social.Network.DomainServices;
using Goods.Social.Network.DomainServices.Realization;
using Goods.Social.Network.DomainServices.Interface;
using Goods.Social.Network.DAL.Repository;
using Goods.Social.Network.DomainModel.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Goods.Social.Network.Web.API.Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<UserContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("UserConnection")).EnableSensitiveDataLogging(), optionsLifetime: ServiceLifetime.Singleton);
            builder.Services.AddDbContextFactory<UserContext>();
            builder.Services.AddSignalR();
            builder.Services.AddControllers();
            builder.Services.AddScoped<IUserService, UserService>();
            //builder.Services.AddMvcCore().AddAuthorization();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IRepository<User>, Repository<User>>();
            builder.Services.AddScoped<IRepository<Role>, Repository<Role>>();

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
                            // укзывает, будет ли валидироваться издатель при валидации токена
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

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseDeveloperExceptionPage();

            app.UseDefaultFiles();
            //app.UseStaticFiles();

            app.UseRouting();
            
            app.UseCors();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
            });

            app.MapControllers();

            app.Run();
        }
    }
}