using System;
using System.Threading.Tasks;
using ChronoPiller.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChronoPiller.Shared.Abstractions;

public abstract class ChronoBaseController : ControllerBase
{
    protected async Task<IActionResult> ExecuteWithErrorHandling(Func<Task<IActionResult>> function)
    {
        try
        {
            return await function();
        }
        catch (UserAlreadyExistsException)
        {
            return Conflict();
        }
        catch (UnauthorizedException)
        {
            return Unauthorized();
        }
        catch (AuthenticationException)
        {
            return Unauthorized();
        }
        catch (AuthorizationException)
        {
            return Forbid();
        }
        catch (ChronoValidationException)
        {
            return BadRequest();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}