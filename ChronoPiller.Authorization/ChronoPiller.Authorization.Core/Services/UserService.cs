using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ChronoPiller.Authorization.Core.DTOs;
using ChronoPiller.Authorization.Core.Entities;
using ChronoPiller.Authorization.Core.Interface;
using ChronoPiller.Authorization.Core.Models;
using ChronoPiller.Authorization.Core.Models.Filters;
using ChronoPiller.Shared.Authorization;
using ChronoPiller.Shared.Enums;
using ChronoPiller.Shared.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace ChronoPiller.Authorization.Core.Services;

public class UserService(
    IConfiguration configuration,
    IUserRepository repository,
    ILogger<UserService> logger) : IUserService
{
    public async Task CreateUser(RegisterModel model)
    {
        await Execute(async () =>
        {
            User user = User.Create(model.Email, model.UserName, model.FirstName, model.LastName);

            if ((await repository.GetUserByFilter(new () { UserName = user.UserName, Email = model.Email })).Any())
                throw new UserAlreadyExistsException(user.Email, user.Surname);
            
            user.PasswordHash = HashString(model.Password);

            user.Role = Roles.User;

            await repository.CreateUser(user);
        });
    }


    public async Task<ChronoTokenData> Login(LoginModel model)
    {
        return await Execute(async () =>
        {
            User user = await repository.GetUserByEmail(model.Email);
            if (user is null)
                throw new UnauthorizedException(model.Email);

            string hashString = HashString(model.Password);

            if (hashString != user.PasswordHash)
                throw new UnauthorizedException(model.Email);

            var claims = new List<Claim>()
            {
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new (ClaimTypes.Role, user.Role.ToString())
            };

            var token = GetToken(claims);
            
            return new ChronoTokenData()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                UserName = user.UserName,
                ExpiresAt = token.ValidTo,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Role = user.Role
            };
        });
    }

    public async Task<IEnumerable<User>> GetByFilterAsync(UserFilter filterRequest) =>
        await Execute(async () =>
        {
            var result = (await repository.GetUserByFilter(filterRequest));
            return result;
        });

    public async Task DeleteUser(int id)
    {
        await Execute(async () => await repository.DeleteUser(id));
    }

    public async Task UpdateUser(UserUpdateModel model)
    {
        // var model = request.Adapt<UserUpdateModel>();
        await repository.UpdateUser(model);
    }

    public async Task<User> GetById(int id)
    {
        return await repository.GetById(id);    
        // return model.Adapt<UserResponse>();
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        SymmetricSecurityKey authSigningKey = 
            new (Encoding.UTF8.GetBytes(configuration["TokenData:Secret"]));

        var token = new JwtSecurityToken(
            issuer: configuration["TokenData:ValidIssuer"],
            audience: configuration["TokenData:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

    private string HashString(string text)
    {
        if (String.IsNullOrEmpty(text))
            throw new MissingMandatoryPropertyException<User>("Password");

        using var sha = SHA256.Create();
        byte[] textBytes = Encoding.UTF8.GetBytes(text);
        byte[] hashBytes = sha.ComputeHash(textBytes);
        
        string hash = BitConverter
            .ToString(hashBytes)
            .Replace("-", String.Empty);

        return hash;
    }
    
    private async Task Execute(Func<Task> function)
    {
        try
        {
            await function();
        }
        catch (ChronoValidationException validationException)
        {
            logger.LogError(validationException, "Exception!");
            throw;
        }
        catch (UnauthorizedException unauthorizedException)
        {
            logger.LogError(unauthorizedException, "Exception!");
            throw;

        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Exception!");
            throw;
        }
    }

    private async Task<T> Execute<T>(Func<Task<T>> function)
    {
        try
        {
            return await function();
        }
        catch (ChronoValidationException validationException)
        {
            logger.LogError(validationException, "Exception!");
            throw;
        }
        catch (UnauthorizedException unauthorizedException)
        {
            logger.LogError(unauthorizedException, "Exception!");
            throw;

        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Exception!");
            throw;
        }
    }
}