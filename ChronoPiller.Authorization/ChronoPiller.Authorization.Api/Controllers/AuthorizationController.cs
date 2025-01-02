using ChronoPiller.Authorization.Api.Interfaces;
using ChronoPiller.Authorization.Api.Services;
using ChronoPiller.Authorization.Core.Models;
using ChronoPiller.Shared.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ChronoPiller.Authorization.Api.Controllers
{
    [ApiController]
    [Route("authorization")]
    public class AuthorizationController(IUserApiService userService) : ChronoBaseController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            return await ExecuteWithErrorHandling(async () =>
            {
                TokenResponse result = await userService.Login(model);
                return Ok(result);
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            return await ExecuteWithErrorHandling(async () =>
            {
                await userService.CreateUser(model);
                return Ok();
            });
        }
    }
}