using ChronoPiller.Api.Core.Entities;
using ChronoPiller.Api.Core.Interface;
using ChronoPiller.Api.Core.Services;
using ChronoPiller.Api.Interfaces;
using ChronoPiller.Api.Services;
using ChronoPiller.Infrastructure.Database;
using ChronoPiller.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();

builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddScoped<IPrescriptionApiService, PrescriptionApiService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseDependencyInjection();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();