using ChronoPiller.Api.Core.Exceptions;
using ChronoPiller.Api.Interfaces;
using ChronoPiller.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChronoPiller.Api.Extensions;

public static class WebApplicationsExtensions
{
    public static void MapRestApi(this WebApplication application)
    {
        MapAuthorizationEndpoints();
        MapUsersEndpoints();
        MapPrescriptionsEndpoints();
        
        void MapPrescriptionsEndpoints()
        {
            application.MapGet("/prescriptions/{id}", async (IPrescriptionApiService prescriptionApiService, Guid id) =>
            {
                return await ExecuteWithErrorHandling(async () =>
                {
                    var prescription = await prescriptionApiService.GetPrescriptionById(id);
                    return Results.Ok(prescription);    
                });
        
            }).RequireAuthorization();
        
            application.MapGet("/users/{id}/prescriptions", async (IPrescriptionApiService prescriptionApiService, Guid id) =>
            {
                return await ExecuteWithErrorHandling(async () =>
                {
                    List<PrescriptionDto> prescription = await prescriptionApiService.GetPrescriptionsByUserId(id);
                    return Results.Ok(
                        new Result<List<PrescriptionDto>>()
                        {
                            Items = prescription
                        });    
                });
        
            }).RequireAuthorization();
        
            application.MapPost("/prescriptions", async (IPrescriptionApiService prescriptionApiService, PrescriptionCreateDto prescription) =>
            {
                return await ExecuteWithErrorHandling(async () =>
                {
                    var createdPrescription = await prescriptionApiService.CreatePrescription(prescription);
                    return Results.Created($"/prescriptions/{createdPrescription.Id}", createdPrescription);
                });
            });
        
            application.MapDelete("/prescriptions/{id}", async (IPrescriptionApiService prescriptionApiService, Guid id) =>
            {
                return await ExecuteWithErrorHandling(async () =>
                {
                    await prescriptionApiService.DeletePrescription(id);
                    return Results.NoContent();
                });
            });
        }

        void MapAuthorizationEndpoints()
        {
            application.MapPost("/authorization/login",
                async (IUserManagementService userService, LoginModel model) =>
                {
                    return await ExecuteWithErrorHandling(async () =>
                    {
                        TokenResponse result = await userService.Login(model);
                        return Results.Ok(result);
                    });
                });

            application.MapPost("/authorization/register",
                async (IUserManagementService userService, RegisterModel model) => await ExecuteWithErrorHandling(async () =>
                {
                    await userService.CreateUser(model);
                    return Results.Ok();
                }));

        }
        
         void MapUsersEndpoints()
        {
            application.MapGet("/users", [Authorize]
                async (IUserManagementService usersService, [AsParameters] UserRequest model) =>
                {
                    return await ExecuteWithErrorHandling(async () =>
                    {
                        IEnumerable<UserResponse> result = await usersService.GetByFilterAsync(model);
                        return Results.Ok(result.ToList());
                    });
                });

            application.MapGet("/users/{id}", [Authorize]
                async (IUserManagementService usersService, Guid id) => await ExecuteWithErrorHandling(async () =>
                {
                    UserResponse result = await usersService.GetById(id);
                    return Results.Ok(result);
                }));

            application.MapDelete("/users/{id}", [Authorize(Roles = "Admin")]
                async (IUserManagementService usersService, Guid id) => await ExecuteWithErrorHandling(async () =>
                {
                    await usersService.DeleteUser(id);
                    return Results.Ok();
                }));

            application.MapPut("/users", 
                async (IUserManagementService usersService, [FromBody] UserUpdateRequest request) => await ExecuteWithErrorHandling(async () =>
                {
                    await usersService.UpdateUser(request);
                    return Results.Ok();
                }));
        }
        
        async Task<IResult> ExecuteWithErrorHandling(Func<Task<IResult>> function)
        {
            try
            {
                return await function();
            }
            catch (UserAlreadyExistsException)
            {
                return Results.Conflict();
            }
            catch (AuthenticationException)
            {
                return Results.Unauthorized();
            }
            catch (AuthorizationException)
            {
                return Results.Forbid();
            }
            catch (ValidationException)
            {
                return Results.BadRequest();
            }
            catch (NotFoundException)
            {
                return Results.NotFound();
            }
            catch (Exception)
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

}