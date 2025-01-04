using ChronoPiller.Authorization.Core.DTOs;
using ChronoPiller.Authorization.Core.Models;
using ChronoPiller.Authorization.Core.Models.Filters;
using ChronoPiller.Shared.Authorization;

namespace ChronoPiller.Authorization.Api.Interfaces;

public interface IUserApiService
{
    public Task CreateUser(RegisterModel model);
    public Task<ChronoTokenData> Login(LoginModel model);
    public Task<IEnumerable<ChronoUserResponse>> GetByFilterAsync(UserFilter filterRequest);
    public Task DeleteUser(int id);
    public Task UpdateUser(UserUpdateModel model);
    public Task<ChronoUserResponse> GetById(int id);
}