using ETicaret.Categories.Data;
using ETicaret.Categories.Data.Interfaces;
using ETicaret.Categories.Repositories;
using ETicaret.Categories.Repositories.Interface;
using ETicaret.Categories.Settings;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<CategoryDatabaseSettings>(builder.Configuration.GetSection(nameof(CategoryDatabaseSettings)));
builder.Services.AddSingleton<ICategoryDatabaseSettings>(sp=>sp.GetRequiredService<IOptions<CategoryDatabaseSettings>>().Value);
// Add services to the container.
builder.Services.AddTransient<ICategoryContext,CategoryContext>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddControllers();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
