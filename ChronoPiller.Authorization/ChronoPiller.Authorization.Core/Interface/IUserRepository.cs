using System.Collections.Generic;
using System.Threading.Tasks;
using ChronoPiller.Authorization.Core.DTOs;
using ChronoPiller.Authorization.Core.Entities;
using ChronoPiller.Authorization.Core.Models.Filters;

namespace ChronoPiller.Authorization.Core.Interface;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUserByFilter(UserFilter filter);
    Task<User> GetById(int id);
    Task<User> GetUserByEmail(string email);
    Task CreateUser(User user);
    Task DeleteUser(int id);
    Task UpdateUser(UserUpdateModel model);
}