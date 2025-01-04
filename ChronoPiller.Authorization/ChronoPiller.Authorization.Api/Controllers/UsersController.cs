using ChronoPiller.Authorization.Api.Interfaces;
using ChronoPiller.Authorization.Api.Models;
using ChronoPiller.Authorization.Core.DTOs;
using ChronoPiller.Authorization.Core.Models;
using ChronoPiller.Authorization.Core.Models.Filters;
using ChronoPiller.Shared.Abstractions;
using ChronoPiller.Shared.Authorization;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChronoPiller.Authorization.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController(IUserApiService userService) : ChronoBaseController
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUsers([FromQuery] UserFilterRequest model)
        {
            return await ExecuteWithErrorHandling(async () =>
            {
                var filter = model.Adapt<UserFilter>();
                IEnumerable<ChronoUserResponse> result = await userService.GetByFilterAsync(filter);
                return Ok(result.ToList());
            });
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            return await ExecuteWithErrorHandling(async () =>
            {
                ChronoUserResponse result = await userService.GetById(id);
                return Ok(result);
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
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
                var model = request.Adapt<UserUpdateModel>();
                await userService.UpdateUser(model);
                return Ok();
            });
        }
    }
}