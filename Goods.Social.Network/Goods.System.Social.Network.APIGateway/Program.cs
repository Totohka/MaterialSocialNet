using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Goods.System.Social.Network.APIGateway;

var builder = WebApplication.CreateBuilder(args);

//1
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
        .AddJsonFile("ocelot.json", optional:false, reloadOnChange:true);
// Add services to the container.

builder.Services.AddControllers();
builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "Gatewaylogger.txt"));
//2
builder.Services.AddOcelot();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//3
app.UseOcelot().Wait();

app.Run();
