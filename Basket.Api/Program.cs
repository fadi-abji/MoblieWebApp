using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add Redis ConnectionMultiplexer to the container
var redisConnection = builder.Configuration.GetConnectionString("RedisConnection");

var redis = ConnectionMultiplexer.Connect(redisConnection); 
builder.Services.AddSingleton<IConnectionMultiplexer>(redis);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
