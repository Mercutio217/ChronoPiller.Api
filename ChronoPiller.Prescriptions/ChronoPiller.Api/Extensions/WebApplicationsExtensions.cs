using ChronoPiller.Api.Core.Exceptions;
using ChronoPiller.Api.Interfaces;
using ChronoPiller.Api.Models;
using ChronoPiller.Api.Models.CreateRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChronoPiller.Api.Extensions;

public static class WebApplicationsExtensions
{
    public static void MapRestApi(this WebApplication application)
    {
        MapPrescriptionsEndpoints();
        application.MapPost("/prescription-items/{id}/count", async (IPrescriptionApiService prescriptionApiService, Guid prescriptionItemId, int pillsCount) =>
        {
            return await ExecuteWithErrorHandling(async () =>
            {
                await prescriptionApiService.SubtractPills(prescriptionItemId, pillsCount);
                return Results.Ok();   
            });
        });
        application.MapGet("users/{userId}/notification-schedules/", async (INotificationScheduleApiService notificationScheduleApiService, Guid userId) =>
        {
            return await ExecuteWithErrorHandling(async () =>
            {
                List<NotificationScheduleDto?> schedules = await notificationScheduleApiService.GetAllNotificationSchedulesByUserId(userId);
                return Results.Ok(schedules);
            });
        });

        application.MapPost("/notification-schedules", async (INotificationScheduleApiService notificationScheduleApiService, NotificationScheduleDto notification) =>
        {
            return await ExecuteWithErrorHandling(async () =>
            {
                await notificationScheduleApiService.AddNotification(notification);
                return Results.Ok();
            });
        });

        application.MapPut("/notification-schedules", async (INotificationScheduleApiService notificationScheduleApiService, NotificationScheduleDto notification) =>
        {
            return await ExecuteWithErrorHandling(async () =>
            {
                await notificationScheduleApiService.UpdateNotification(notification);
                return Results.Ok();
            });
        });

        application.MapDelete("/notification-schedules/{id}", async (INotificationScheduleApiService notificationScheduleApiService, Guid id) =>
        {
            return await ExecuteWithErrorHandling(async () =>
            {
                await notificationScheduleApiService.DeleteNotification(id);
                return Results.Ok();
            });
        });
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

         void MapNotificationsEndpoints()
         {            
             application.MapPut("/notifications", 
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
            catch (UnauthorizedException)
            {
                return Results.Unauthorized();
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
            catch (Exception e)
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

}