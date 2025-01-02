using ChronoPiller.Authorization.Core.DTOs;
using ChronoPiller.Authorization.Core.Models;
using ChronoPiller.Authorization.Core.Models.Filters;

namespace ChronoPiller.Authorization.Api.Services;

public interface IUserApiService
{
    public Task CreateUser(RegisterModel model);
    public Task<TokenResponse> Login(LoginModel model);
    public Task<IEnumerable<UserResponse>> GetByFilterAsync(UserFilter filterRequest);
    public Task DeleteUser(int id);
    public Task UpdateUser(UserUpdateModel model);
    public Task<UserResponse> GetById(int id);
}