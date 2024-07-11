using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChronoPiller.Api.Core.DTOs;
using ChronoPiller.Api.Core.Entities;
using ChronoPiller.Api.Core.Exceptions;
using ChronoPiller.Api.Core.Filters;
using ChronoPiller.Api.Core.Interface;
using ChronoPiller.Api.Interfaces;
using ChronoPiller.Api.Models;
using Mapster;
using Microsoft.IdentityModel.Tokens;

namespace ChronoPiller.Api.Services;

public class UserManagementService : IUserManagementService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _repository;
    private ILogger<UserManagementService> _logger;

    public UserManagementService(
        IConfiguration configuration,
        IUserRepository repository, ILogger<UserManagementService> logger)
    {
        _configuration = configuration;
        _repository = repository;
        _logger = logger;
    }

    public async Task CreateUser(RegisterModel model)
    {
        await Execute(async () =>
        {
            User? user = User.Create(model.Email, model.UserName, model.FirstName, model.LastName);

            if (await _repository.GetUserByEmail(user.Email) is not null ||
                (await _repository.GetUserByFilter(new () { UserName = user.UserName })).Any())
                throw new UserAlreadyExistsException(user.Email, user.Surname);
            
            string hash = HashString(model.Password);

            user.PasswordHash = hash;

            var role = await _repository.GetRoleByName("User");
            user.Roles = new List<Role>() { role };

            await _repository.CreateUser(user);
        });
    }


    public async Task<TokenResponse> Login(LoginModel model)
    {
        return await Execute(async () =>
        {
            
            User? user = await _repository.GetUserByEmail(model.Email);
            if (user is null)
                throw new UnauthorizedException(model.Email);

            string hashString = HashString(model.Password);

            if (hashString != user.PasswordHash)
                throw new UnauthorizedException(model.Email);

            var claims = new List<Claim>()
            {
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var token = GetToken(claims);
            
            return new TokenResponse()
            {
                ExpiresAt = token.ValidTo,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Roles = user.Roles.Select(r => r.Name),
                UserData = new ()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    Surname = user.Surname,
                    UserName = user.UserName
                }
            };
        });
    }

    public async Task<IEnumerable<UserResponse>> GetByFilterAsync(UserRequest request) =>
        await Execute(async () =>
        {
            var result = (await _repository.GetUserByFilter(request.Adapt<UserFilter>())).Adapt<IEnumerable<UserResponse>>();
            return result;
        });

    public async Task DeleteUser(Guid id)
    {
        await Execute(async () => await _repository.DeleteUser(id));
    }

    public async Task UpdateUser(UserUpdateRequest request)
    {
        var model = request.Adapt<UserUpdateModel>();
        await _repository.UpdateUser(model);
    }

    public async Task<UserResponse> GetById(Guid id)
    {
        User model = await _repository.GetById(id);    
        return model.Adapt<UserResponse>();
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        SymmetricSecurityKey authSigningKey = 
            new (Encoding.UTF8.GetBytes(_configuration["TokenData:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["TokenData:ValidIssuer"],
            audience: _configuration["TokenData:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
    private  string HashString(string text)
    {
        if (String.IsNullOrEmpty(text))
            throw new MissingMandatoryPropertyException<User>("Password");

        using var sha = new System.Security.Cryptography.SHA256Managed();
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
        catch (ApplicationValidationException validationException)
        {
            _logger.LogError(validationException, "Exception!");
            throw;
        }
        catch (UnauthorizedException unauthorizedException)
        {
            _logger.LogError(unauthorizedException, "Exception!");
            throw;

        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception!");
            throw;
        }
    }

    private async Task<T> Execute<T>(Func<Task<T>> function)
    {
        try
        {
            return await function();
        }
        catch (ApplicationValidationException validationException)
        {
            _logger.LogError(validationException, "Exception!");
            throw;
        }
        catch (UnauthorizedException unauthorizedException)
        {
            _logger.LogError(unauthorizedException, "Exception!");
            throw;

        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception!");
            throw;
        }
    }
}