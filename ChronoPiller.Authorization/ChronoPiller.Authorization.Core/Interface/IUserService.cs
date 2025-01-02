using System.Collections.Generic;
using System.Threading.Tasks;
using ChronoPiller.Authorization.Core.DTOs;
using ChronoPiller.Authorization.Core.Entities;
using ChronoPiller.Authorization.Core.Models;
using ChronoPiller.Authorization.Core.Models.Filters;

namespace ChronoPiller.Authorization.Core.Interface;

public interface IUserService
{
    Task CreateUser(RegisterModel model);
    Task<TokenResponse> Login(LoginModel model);
    Task<IEnumerable<User>> GetByFilterAsync(UserFilter filterRequest);
    Task DeleteUser(int id);
    Task UpdateUser(UserUpdateModel model);
    Task<User> GetById(int id);
}