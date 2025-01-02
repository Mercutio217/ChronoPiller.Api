using ChronoPiller.Authorization.Api.Interfaces;
using ChronoPiller.Authorization.Core.Models;
using ChronoPiller.Shared.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChronoPiller.Authorization.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController(IUserManagementService userService) : ChronoBaseController
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUsers([FromQuery] UserFilterRequest model)
        {
            return await ExecuteWithErrorHandling(async () =>
            {
                IEnumerable<UserResponse> result = await userService.GetByFilterAsync(model);
                return Ok(result.ToList());
            });
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            return await ExecuteWithErrorHandling(async () =>
            {
                UserResponse result = await userService.GetById(id);
                return Ok(result);
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            return await ExecuteWithErrorHandling(async () =>
            {
                await userService.DeleteUser(id);
                return Ok();
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserFilterUpdateRequest request)
        {
            return await ExecuteWithErrorHandling(async () =>
            {
                await userService.UpdateUser(request);
                return Ok();
            });
        }
    }
}