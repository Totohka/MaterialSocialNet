using Goods.Social.Network.DAL;
using Goods.Social.Network.DomainModel;
using Microsoft.EntityFrameworkCore;
using Goods.Social.Network.DomainServices;
using Goods.Social.Network.DomainServices.Realization;
using Goods.Social.Network.DomainServices.Interface;
using Goods.Social.Network.DAL.Repository;
using Goods.Social.Network.DomainModel.Entities;
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

            builder.Services.AddControllers();
            builder.Services.AddScoped<IUserService, UserService>();
            //builder.Services.AddMvcCore().AddAuthorization();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
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

            var app = builder.Build();

            app.UseDeveloperExceptionPage();

            app.UseDefaultFiles();
            app.UseStaticFiles();

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