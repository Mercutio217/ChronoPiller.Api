using ChronoPiller.Api.Core.DTOs;
using ChronoPiller.Api.Core.Entities;
using ChronoPiller.Api.Core.Filters;

namespace ChronoPiller.Api.Core.Interface;

public interface IUserRepository
{
    Task CreateUser(User? user);
    Task<IEnumerable<User?>> GetUserByFilter(UserFilter filter);
    Task<User?> GetUserByEmail(string email);
    Task DeleteUser(Guid id);
    Task<Role?> GetRoleByName(string name);
    Task UpdateUser(UserUpdateModel model);
    Task<User> GetById(Guid id);
}