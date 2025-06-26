using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using CardCollector_backend.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "CardCollector API", Description = "", Version = "v1" });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<CardContext>(opt=> opt.UseInMemoryDatabase("CardList"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "CardCollector API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
