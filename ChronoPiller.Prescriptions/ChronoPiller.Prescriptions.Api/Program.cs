using ChronoPiller.Api;
using ChronoPiller.Api.Core.Interface;
using ChronoPiller.Api.Core.Services;
using ChronoPiller.Api.Interfaces;
using ChronoPiller.Api.Services;
using ChronoPiller.Infrastructure.Database;
using ChronoPiller.Infrastructure.Repositories;
using ChronoPiller.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging();
builder.AddChronoAuthorization();

builder.AddChronoSwagger("ChronoPiller.Prescriptions.Api", "v1");
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddScoped<IPrescriptionApiService, PrescriptionApiService>();

builder.Services.RegisterMapsterConfiguration();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.Run();
