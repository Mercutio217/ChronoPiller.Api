using System.Text;
using ChronoPiller.Shared.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ChronoPiller.Shared.Extensions;

public static class ChronoConfigurationExtensions
{
    public static void AddChronoSwagger(this IHostApplicationBuilder builder, string appName, string version)
    {
        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc(version, new OpenApiInfo { Title = appName, Version = version });
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
                    []
                }
            });
        });
    }

    public static void AddChronoAuthorization(this IHostApplicationBuilder builder)
    {
        VerifyConfiguration(builder);
    
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
                ValidAudience = builder.Configuration["TokenData:Issuer"]!,
                ValidIssuer = builder.Configuration["TokenData:Audience"]!,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenData:Secret"]!))
            };
        });
        builder.Services.AddAuthorization();
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(opt => opt.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
        });
    }

    private static void VerifyConfiguration(IHostApplicationBuilder builder)
    {
        var secret = builder.Configuration["TokenData:Secret"];
        var issuer = builder.Configuration["TokenData:Issuer"];
        var audience = builder.Configuration["TokenData:Audience"];
        
        if(secret is null || issuer is null || audience == null)
        {
            throw new MissingAuthorizationDataException();
        }
    }
}