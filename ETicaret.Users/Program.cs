using ETicaret.Users.Data;
using ETicaret.Users.Data.Interface;
using ETicaret.Users.Repositories;
using ETicaret.Users.Repositories.Interface;
using ETicaret.Users.Settings;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<UserDatabaseSettings>(builder.Configuration.GetSection(nameof(UserDatabaseSettings)));
builder.Services.AddSingleton<IUserDatabaseSettings>(sp => sp.GetRequiredService<IOptions<UserDatabaseSettings>>().Value);
builder.Services.AddTransient<IUserRepository,UserRepository>();
builder.Services.AddTransient<IUserContext, UserContext>();

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

app.Run();
