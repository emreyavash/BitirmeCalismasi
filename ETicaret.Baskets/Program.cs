using ETicaret.Baskets.Extensions;
using ETicaret.Baskets.Repositories;
using ETicaret.Baskets.Repositories.Interface;
using EventBusRabbitMQ.Producer;
using EventBusRabbitMQ;
using RabbitMQ.Client;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSingleton<ConnectionMultiplexer>(s =>
//{
//    var redisConf = ConfigurationOptions.Parse(builder.Configuration["RedisSettings:ConnectionString"], true);
//    redisConf.ResolveDns = true;
//    redisConf.AbortOnConnectFail = false;
//    return ConnectionMultiplexer.Connect(redisConf);

//});
builder.Services.AddSingleton(sp=>sp.ConfigureRedis(builder.Configuration));
//builder.Services.AddSingleton<IRedisRegistration,RedisRegistration>();

builder.Services.AddTransient<IBasketRepository, BasketRepository>();

builder.Services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
{
    var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
    var factory = new ConnectionFactory()
    {
        HostName = builder.Configuration["EventBus:HostName"]
    };
    if (!string.IsNullOrWhiteSpace(builder.Configuration["EventBus:UserName"]))
    {
        factory.UserName = builder.Configuration["EventBus:UserName"];
    }
    if (!string.IsNullOrWhiteSpace(builder.Configuration["EventBus:Password"]))
    {
        factory.Password = builder.Configuration["EventBus:Password"];
    }
    var retryCount = 5;
    if (!string.IsNullOrWhiteSpace(builder.Configuration["EventBus:RetryCount"]))
    {
        retryCount = int.Parse(builder.Configuration["EventBus:RetryCount"]);
    }
    return new DefaultRabbitMQPersistentConnection(factory, retryCount, logger);
});

builder.Services.AddSingleton<EventBusRabbitMQProducer>();
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

app.Run();
