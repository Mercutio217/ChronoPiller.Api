using System.Text;
using ChronoPiller.Api;
using ChronoPiller.Api.Core.Entities;
using ChronoPiller.Api.Core.Exceptions;
using ChronoPiller.Api.Core.Interface;
using ChronoPiller.Api.Core.Services;
using ChronoPiller.Api.Extensions;
using ChronoPiller.Api.Interfaces;
using ChronoPiller.Api.Models;
using ChronoPiller.Api.Services;
using ChronoPiller.Infrastructure.Database;
using ChronoPiller.Infrastructure.Repositories;
using ChronoPiller.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidAudience = builder.Configuration["TokenData:Issuer"],
        ValidIssuer = builder.Configuration["TokenData:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenData:Secret"]))
    };
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(opt => opt.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new ()
    {
        {
            new ()
            {
                Reference = new ()
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserManagementService, UserManagementService>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddScoped<IPrescriptionApiService, PrescriptionApiService>();
builder.Services.RegisterMapsterConfiguration();

var app = builder.Build();

app.MapRestApi();

// Configure the HTTP request pipeline.
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
    DatabaseOptions? settings = builder.Configuration.GetSection("Database").Get<DatabaseOptions>();
    var database = new ApplicationDbContext(Options.Create(settings));
    database.Database.EnsureCreated();
    

    database.SaveChanges();
    
    if (!database.Roles.Any())
    {
        database.Roles.Add(new Role("Admin"));
        database.Roles.Add(new Role("User"));
    
    }
    
    database.SaveChanges();
    
    if (!database.Users.Any())
    {
        var adminUser = 
            User.Create("sgdukat@hotmail.com","Gul" ,"Skrain", "Dukat");
    
        adminUser.PasswordHash = "799DBF90EE52688EB50516DE263415C05207AD00866860331795784F1EC950CF";
        adminUser.Roles = new List<Role>() { database.Roles.First(r => r.Name == "Admin") };
        database.Users.Add(adminUser);
    }

    database.SaveChanges();
}