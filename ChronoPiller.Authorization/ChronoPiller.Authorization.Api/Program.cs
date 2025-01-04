using ChronoPiller.Authorization.Api.Interfaces;
using ChronoPiller.Authorization.Api.Services;
using ChronoPiller.Authorization.Core.Entities;
using ChronoPiller.Authorization.Core.Interface;
using ChronoPiller.Authorization.Core.Services;
using ChronoPiller.Authorization.Infrastructure.Database;
using ChronoPiller.Authorization.Infrastructure.Repositories;
using ChronoPiller.Authorization.Infrastructure.Settings;
using ChronoPiller.Shared.Extensions;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.AddChronoAuthorization();
builder.AddChronoSwagger("ChronoPiller.Authorization.Api", "v1");
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserApiService, UserApiService>();
builder.Services.Configure<DatabaseOptions>(
    builder.Configuration.GetSection("ConnectionStrings"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    SetupDatabase();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.Run();


void SetupDatabase()
{
    DatabaseOptions? settings = builder.Configuration.GetSection("ConnectionStrings").Get<DatabaseOptions>();
    if (settings is null)
        return;

    var database = new ApplicationDbContext(Options.Create(settings));
    database.Database.EnsureCreated();
    
    database.SaveChanges();
    
    if (!database.Users.Any())
    {
        var adminUser = 
            User.Create("sg@gmail.com","Gul" ,"Skrain", "Dukat");
    
        adminUser.PasswordHash = "799DBF90EE52688EB50516DE263415C05207AD00866860331795784F1EC950CF";
        database.Users.Add(adminUser);
    }

    database.SaveChanges();
}