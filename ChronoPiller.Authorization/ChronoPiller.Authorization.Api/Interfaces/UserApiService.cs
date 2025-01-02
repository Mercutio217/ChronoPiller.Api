using ChronoPiller.Authorization.Core.DTOs;
using ChronoPiller.Authorization.Core.Interface;
using ChronoPiller.Authorization.Core.Models;
using ChronoPiller.Authorization.Core.Models.Filters;
using Mapster;

namespace ChronoPiller.Authorization.Api.Services;

public class UserApiService(IUserService userService) : IUserApiService
{
    public async Task CreateUser(RegisterModel model)
    {
        await userService.CreateUser(model);
    }

    public async Task<TokenResponse> Login(LoginModel model)
    {
        return await userService.Login(model);
    }

    public async Task<IEnumerable<UserResponse>> GetByFilterAsync(UserFilter filterRequest)
    {
        return (await userService.GetByFilterAsync(filterRequest)).Adapt<IEnumerable<UserResponse>>();
    }

    public async Task DeleteUser(int id)
    {
        await userService.DeleteUser(id);
    }

    public async Task UpdateUser(UserUpdateModel model)
    {
        await userService.UpdateUser(model);
    }

    public async Task<UserResponse> GetById(int id)
    {
        return (await userService.GetById(id)).Adapt<UserResponse>();
    }
    
}