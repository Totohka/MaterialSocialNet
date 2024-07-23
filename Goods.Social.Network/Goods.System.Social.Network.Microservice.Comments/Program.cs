using DAL.Repository.Comments.Implementation;
using DAL.Repository.Comments.Interface;
using DomainServices.Comments.Implementation;
using DomainServices.Comments.Interface;
using Goods.System.Social.Network.DAL;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.Microservice.Comments.Infrastructure.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddControllers();
builder.Services.AddScoped<ICommentEntityService<CommentPost>, CommentPostService<CommentPost>>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentPostRepository, CommentPostRepository> ();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(typeof(ProfileCommentPost));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
