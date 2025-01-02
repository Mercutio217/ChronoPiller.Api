using ChronoPiller.Authorization.Core.Models;

namespace ChronoPiller.Authorization.Api.Interfaces;

public interface IUserManagementService
{
    public Task CreateUser(RegisterModel model);
    public Task<TokenResponse> Login(LoginModel model);
    public Task<IEnumerable<UserResponse>> GetByFilterAsync(UserFilterRequest filterRequest);
    public Task DeleteUser(Guid id);

    public Task UpdateUser(UserFilterUpdateRequest request);
    public Task<UserResponse> GetById(Guid id); }